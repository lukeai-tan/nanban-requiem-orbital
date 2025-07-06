using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Prts : Boss
{

    // Hp thresholds
    public event EventHandler Half;
    public event EventHandler Zero;
    public Priestess girlboss;

    // Skills
    [Export] protected PackedScene projectileScene;
    [Export] protected PackedScene debuffScene;
    [Export] protected PackedScene areaScene;
    protected BasicRangedBuff attack1;
    // protected AOEMeleeAttack attack2;
    protected TowerClosestToSelf targeting;
    // public event EventHandler LockUI;
    // public event EventHandler LockDeployment;
    // public event EventHandler Finale;
    protected bool shield = false;
    protected int shieldHp = 0;
    [Export] protected int maxShieldHp;
    public event EventHandler Corrode;

    public override void _Ready()
    {
        base._Ready();
        this.invulnerable = true;
        this.incapacitated = true;
        this.targetable = false;

        // Wipes all enemies / towers in a horizontal / vertical strip
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("Helios"));

        // Turns one of your units hostile
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("Achlys"));

        // Unending machine gun attack until you "break" the "loop" (shield)
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("Astrape"));

        // Hot potato with units. You must "return" the bomb by relocating to her
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("Charybdis"));

        // Simon says, or in reverse? Read the if else statement
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("Pharos"));
    }

    public override void SetActions()
    {

    }

    public void Connect(Priestess girlboss)
    {
        this.girlboss = girlboss;
        this.girlboss.Computation += this.Activate;
    }

    public void Activate(object gb, EventArgs e)
    {
        this.health = this.maxHealth;
        this.healthBar.Value = this.maxHealth;
        this.invulnerable = false;
        this.incapacitated = false;
        this.targetable = true;
    }

    public override void _Process(double delta)
    {
        if (!this.incapacitated)
        {
            if (this.timeSinceLastSkill >= this.skillcooldown)
            {
                // this.incapacitated = true;
                this.Act();
            }
            this.timeSinceLastSkill += delta;
            this.Corrode?.Invoke(this, EventArgs.Empty);
        }
    }

    // Wipes all enemies / towers in a horizontal / vertical strip
    public async void Helios()
    {

    }

    // Turns one of your units hostile
    public async void Achlys()
    {

    }

    // Unending machine gun attack until you "break" the "loop" (shield)
    public async void Astrape()
    {
        GD.Print("Astrape");
        this.incapacitated = true;
        // this.animation.Play("");
        this.shieldHp = this.maxShieldHp;
        this.shield = true;
        while (shield)
        {
            // attack
            await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        }
        this.Recover();
    }

    // Hot potato with units. You must "return" the bomb by relocating to her
    public async void Charybdis()
    {

    }

    // Simon says, or in reverse? Read the if else statement
    public async void Pharos()
    {

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
            if (this.shieldHp > damage)
            {
                this.shieldHp -= damage;
            }
            else
            {
                this.shieldHp = 0;
                this.shield = false;
            }
        }
        base.TakeDamage(damage);
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
    protected void Deactivate()
    {
        this.invulnerable = true;
        this.incapacitated = true;
        this.targetable = false;
    }

    public override void _ExitTree()
    {
        this.girlboss.Computation -= this.Activate;
    }

}