using System;
using System.Collections.Generic;
using System.Linq;
using Godot;


public abstract partial class Boss : Enemy, IAct
{

    protected bool invulnerable = false;
    protected bool incapacitated = false;
    protected double timeSinceLastSkill = 0;
    [Export] protected double skillcooldown;
    protected List<BossSkill> skills = new List<BossSkill>();
    protected GlobalDetectionRange range;
    public event EventHandler<BoolEventArgs> HasEnemy;
    public event EventHandler<BoolEventArgs> HasTower;
    protected int stoppers = 0;

    public override void _Ready()
    {
        this.maxHealth = this.health;
        this.animation = this.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        this.range = this.GetNodeOrNull<GlobalDetectionRange>("Range");
    }

    public void SetHealthBar(TextureProgressBar healthBar)
    {
        this.healthBar = healthBar;
        this.healthBar.MaxValue = this.maxHealth;
        this.healthBar.Value = this.health;
    }

    public abstract void SetActions();

    protected override void TakeDamage(int damage)
    {
        if (!this.invulnerable)
        {
            int trueDamage = damage > 1 ? damage : 1;
            this.health -= trueDamage;
            this.healthBar.Value = this.health;
            double ratio = (double)this.health / (double)this.maxHealth;
            switch (stoppers)
            {
                case 0:
                    if (ratio <= 0.75)
                    {
                        this.ThreeQF();
                        this.stoppers += 1;
                    }
                    break;
                case 1:
                    if (ratio <= 0.5)
                    {
                        this.HalfF();
                        this.stoppers += 1;
                    }
                    break;
                case 2:
                    if (ratio <= 0.25)
                    {
                        this.OneQF();
                        this.stoppers += 1;
                    }
                    break;
                case 3:
                    if (ratio <= 0)
                    {
                        this.ZeroF();
                    }
                    break;
            }
        }
    }

    protected abstract void ZeroF();

    protected abstract void OneQF();

    protected abstract void HalfF();

    protected abstract void ThreeQF();

    public void Act()
    {
        this.CheckTargets();
        BossSkill skill = this.ChooseSkill();
        if (skill != null)
        {
            skill.Execute();
        }
    }

    protected void CheckTargets()
    {
        bool hasEnemy = this.range.GetAllEnemies().Count > 0;
        bool hasTower = this.range.GetAllTowers().Count > 0;
        this.HasEnemy?.Invoke(this, new BoolEventArgs(hasEnemy));
        this.HasTower?.Invoke(this, new BoolEventArgs(hasTower));
    }

    protected BossSkill ChooseSkill()
    {
        List<BossSkill> usable = this.skills.Where(skill => skill.IsUsable()).ToList();
        usable.Sort((e1, e2) => e1.GetPriority().CompareTo(e2.GetPriority()));
        return usable.LastOrDefault();
    }

    public virtual void Initialize()
    {
        this.pathing = new BossPathing(this);
    }

    public override void ModifyMovementSpeed(double multiplier) { }

    public override void ModifyAtk(double multiplier) { }

    public override float GetProgress()
    {
        return 0;
    }

    public override void _ExitTree() { }

}