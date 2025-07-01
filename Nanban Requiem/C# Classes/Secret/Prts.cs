using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Prts : Boss
{

    public event EventHandler Half;
    public event EventHandler Zero;
    public Priestess girlboss;

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
        this.girlboss.Computation += (object gb, EventArgs e) => this.Activate();
    }

    public void Activate()
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

    }

    // Hot potato with units. You must "return" the bomb by relocating to her
    public async void Charybdis()
    {
        
    }

    // Simon says, or in reverse? Read the if else statement
    public async void Pharos()
    {

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

}