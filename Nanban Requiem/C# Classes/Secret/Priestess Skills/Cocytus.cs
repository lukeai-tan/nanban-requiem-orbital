using System;
using Godot;

public partial class Cocytus : BossSkill
{

    protected Priestess boss;
    [Export] protected double cooldown;
    protected double timeSinceLastUse = 0;

    public override void _Ready()
    {
        this.priority = 1;
        this.usable = true;
        this.boss = this.GetParentOrNull<Priestess>();
    }

    public override void _Process(double delta)
    {
        if (!this.usable && this.timeSinceLastUse >= 1 / this.cooldown)
        {
            this.usable = true;
        }
    }

    public override void Execute()
    {
        this.boss.Cocytus();
        this.usable = false;
        this.timeSinceLastUse = 0;
    }

}