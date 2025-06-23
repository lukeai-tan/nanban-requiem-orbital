using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Prts : Boss
{

    public event EventHandler Half;
    public Priestess girlboss;

    public override void _Ready()
    {
        base._Ready();

        // Wipes all enemies / towers in a horizontal / vertical strip
        this.skills.Add(this.GetNodeOrNull<BossSkill>("NullError"));

        // Turns one of your units hostile
        this.skills.Add(this.GetNodeOrNull<BossSkill>("CosmicBitFlip"));

        // Unending machine gun attack until you "break" the "loop" (shield)
        this.skills.Add(this.GetNodeOrNull<BossSkill>("WhileLoop"));

        // Hot potato with units. You must "return" the bomb by relocating to her
        this.skills.Add(this.GetNodeOrNull<BossSkill>("RecursiveCall"));

        // Simon says, or in reverse? Read the if else statement
        this.skills.Add(this.GetNodeOrNull<BossSkill>("IfElse"));
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
        this.invulnerable = false;
        this.incapacitated = false;
        this.targetable = true;
    }

    public override void _Process(double delta)
    {
        if (!this.incapacitated && this.timeSinceLastSkill >= 1 / this.skillcooldown)
        {
            this.Act();
        }
    }
    
    public override void Act()
    {
        throw new NotImplementedException();
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
        this.girlboss.ExitComputation();
    }

    // After switching back to priestess
    protected void Deactivate()
    {
        this.invulnerable = true;
        this.incapacitated = true;
        this.targetable = false;
        this.girlboss.ExitComputation();
        this.health = this.maxHealth;
    }

}