using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class Prts : Boss
{

    // Hp thresholds
    public event EventHandler Active;
    public event EventHandler Half;
    public event EventHandler Zero;
    public Priestess girlboss;
    public bool active = false;

    // Skills
    [Export] protected PackedScene projectileScene;
    [Export] protected PackedScene debuffScene;
    [Export] protected PackedScene controlDebuffScene;
    [Export] protected PackedScene bombDebuffScene;
    [Export] protected PackedScene wipeScene;
    [Export] protected PackedScene areaScene;
    [Export] protected PackedScene tileScene;
    protected UnitWipe wipe1;
    protected BasicRangedBuff attack1;
    protected BasicMeleeBuff debuff1;
    protected AOEMeleeAttack attack2;
    protected BasicMeleeAttack attack3;
    protected PrtsRandom targeting1;
    protected PrtsControl targeting2;
    protected TowerFurthestFromSelf targeting3;
    protected bool shield = false;
    protected TextureProgressBar shieldBar;
    protected int shieldHp = 0;
    [Export] protected int maxShieldHp;
    protected double timeSinceLastCorrosion = 0;
    public event EventHandler Corrode;

    public override void _Ready()
    {
        base._Ready();
        this.invulnerable = true;
        this.targetable = false;
        this.animation.Play("inactive");
    }

    public void SetShieldBar(TextureProgressBar shieldBar)
    {
        this.shieldBar = shieldBar;
        this.shieldBar.MaxValue = this.maxShieldHp;
    }

    public override void SetActions()
    {
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Achlys"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Charybdis"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Helios"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Pharos"));

        this.targeting1 = new PrtsRandom();
        this.targeting2 = new PrtsControl();
        this.targeting3 = new TowerFurthestFromSelf(this);
        this.targeting1.SetTargets(2);

        this.wipe1 = new UnitWipe();

        this.attack1 = new BasicRangedBuff(this.projectileScene, this, this.debuffScene);
        this.attack1.SetAttackAndSpeed(new PhysicalAttack(), 300);
        this.attack1.SetModifiers(this.attack, 0.4);

        this.debuff1 = new BasicMeleeBuff(this.controlDebuffScene);
        this.debuff1.SetAttack(new PhysicalAttack());

        this.attack2 = new AOEMeleeAttack(this.areaScene);
        this.attack2.SetAttack(new ArtsAttack());
        this.attack2.SetModifiers(this.attack, 1.2);

        this.attack3 = new BasicMeleeAttack();
        this.attack3.SetAttack(new ArtsAttack());
        this.attack3.SetModifiers(this.attack, 0.6);
    }

    public void Connect(Priestess girlboss)
    {
        this.girlboss = girlboss;
        this.girlboss.Computation += this.Activate;
    }

    public async void Activate(object gb, EventArgs e)
    {
        this.Active?.Invoke(this, EventArgs.Empty);
        this.animation.Play("activate");
        this.health = this.maxHealth;
        this.healthBar.Value = this.maxHealth;
        this.invulnerable = false;
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        this.active = true;
        this.targetable = true;
        this.animation.Play("active");
    }

    public override void _Process(double delta)
    {
        if (this.active && !this.incapacitated)
        {
            if (this.timeSinceLastSkill >= this.skillcooldown)
            {
                // this.incapacitated = true;
                this.Act();
            }
            this.timeSinceLastSkill += delta;
            this.timeSinceLastCorrosion += delta;
            if (this.timeSinceLastCorrosion >= 0.1)
            {
                this.Corrode?.Invoke(this, EventArgs.Empty);
                this.timeSinceLastCorrosion = 0;
            }
        }
    }

    // Wipes all enemies / towers in a horizontal / vertical strip
    public async void Helios()
    {
        GD.Print("Helios");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        Node2D projectilesNode = this.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map/Projectiles");
        Vector2 position = this.targeting1.GetTarget(this.range.GetAllTowers()).GlobalPosition;
        Node areaWipe = wipeScene.Instantiate();
        if (areaWipe is GeneralSingleUse effect)
        {
            projectilesNode.AddChild(effect);
            if (GD.Randi() % 2 == 0)
            {
                effect.RotationDegrees = 90;
            }
            effect.Activate(position, this.wipe1);
        }
        this.Recover();
    }

    // Debuffs a tower by making them attack other towers when trying to hit enemies
    public async void Achlys()
    {
        GD.Print("Achlys");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        Tower target = this.targeting2.GetTarget(this.range.GetAllTowers());
        if (target != null)
        {
            this.debuff1.Execute(target);
        }
        this.Recover();
    }

    // Deploy shield followed by continuous attacks while active
    public async void Astrape()
    {
        GD.Print("Astrape");
        this.incapacitated = true;
        // this.animation.Play("");
        this.shieldHp = this.maxShieldHp;
        this.shieldBar.Value = this.maxShieldHp;
        this.shieldBar.Visible = true;
        this.shield = true;
        while (shield)
        {
            Tower target = this.targeting1.GetTarget(this.range.GetAllTowers());
            if (target != null)
            {
                this.attack1.Execute(target);
            }
            await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        }
        await ToSignal(GetTree().CreateTimer(15f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // Hot potato with units. You must "return" the bomb by relocating to her
    public async void Charybdis()
    {
        GD.Print("Charybdis");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        this.Spread(new List<Tower>());
        this.Recover();
    }
    
    protected void Implode(object target, EventArgs e)
    {

    }
    
    protected void Explode(object target, List<Tower> marked)
    {
        if (target is Tower tower)
        {
            marked.Add(tower);
            this.attack2.Execute(tower);
            this.Spread(marked);
        }
    }

    protected async void Spread(List<Tower> marked)
    {
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        List<Tower> targets = this.range.GetAllTowers().Where(tower => !marked.Contains(tower)).ToList();
        Tower target = this.targeting3.GetTarget(targets);
        if (target != null)
        {
            Node debuff = this.bombDebuffScene.Instantiate();
            if (debuff is TowerBomb mark)
            {
                mark.Return += this.Implode;
                mark.Next += (object target, EventArgs e) => this.Explode(target, marked);
                target.ReceiveBuff(mark);
            }
        }
    }

    // 2 / 3 consecutive devastating area attacks with windup
    public async void Pharos(List<Vector2> chosenPosition)
    {
        GD.Print("Pharos");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(1.5f, false), SceneTreeTimer.SignalName.Timeout);
        Node2D projectilesNode = this.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map/Projectiles");
        List<Tower> targets = this.targeting1.GetTargets(this.range.GetAllTowers());
        foreach (Tower target in targets)
        {
            Vector2 targetPosition = target.GlobalPosition;
            foreach (Vector2 position in chosenPosition)
            {
                Node areaEffect = tileScene.Instantiate();
                if (areaEffect is EnemyLasting effect)
                {
                    projectilesNode.AddChild(effect);
                    effect.Activate(targetPosition + position, this.attack3);
                }
            }
        }
        this.Recover();
    }

    protected void Recover()
    {
        this.timeSinceLastSkill = 0;
        this.incapacitated = false;
    }

    protected override void TakeDamage(int damage)
    {
        if (!this.invulnerable && this.shield)
        {
            int trueDamage = damage > 1 ? damage : 1;
            if (this.shieldHp > trueDamage)
            {
                this.shieldHp -= trueDamage;
                this.shieldBar.Value = this.shieldHp;
            }
            else
            {
                this.shieldHp = 0;
                this.shield = false;
                this.shieldBar.Visible = false;
            }
        }
        else
        {
            base.TakeDamage(damage);
        }
    }

    protected override void ThreeQF() { }

    protected override void HalfF()
    {
        this.Half?.Invoke(this, EventArgs.Empty);
    }

    protected override void OneQF() { }

    protected override void ZeroF()
    {
        this.Deactivate();
        this.Zero?.Invoke(this, EventArgs.Empty);
        this.girlboss.ExitComputation();
    }

    // After switching back to priestess
    protected async void Deactivate()
    {
        this.animation.Play("deactivate");
        this.invulnerable = true;
        this.active = false;
        this.targetable = false;
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        this.animation.Play("inactive");
    }

    public override float GetProgress()
    {
        return 0;
    }

    public override void _ExitTree()
    {
        this.girlboss.Computation -= this.Activate;
    }

}