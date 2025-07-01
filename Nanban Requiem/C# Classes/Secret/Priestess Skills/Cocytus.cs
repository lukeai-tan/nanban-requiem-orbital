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
        this.boss = this.GetParentOrNull<Priestess>();
        this.boss.HasEnemy += (object boss, BoolEventArgs e) => this.UseCheck(e.boolean);
    }

    public override void _Process(double delta)
    {
        this.timeSinceLastUse += delta;
    }

    public void UseCheck(bool hasTarget)
    {
        if (hasTarget && this.timeSinceLastUse >= this.cooldown)
        {
            this.usable = true;
        }
        else
        {
            this.usable = false;
        }
    }

    public override void Execute()
    {
        this.boss.Cocytus();
        this.timeSinceLastUse = 0;
    }

}