using System;
using Godot;

public partial class Erebus : BossSkill
{
    
    protected Priestess boss;
    [Export] protected double cooldown;
    protected double timeSinceLastUse = 0;

    public override void _Ready()
    {
        this.priority = 2;
        this.boss = this.GetParentOrNull<Priestess>();
        this.boss.HasEnemy += (object boss, BoolEventArgs e) => this.UseCheck(e.boolean);
    }
    
    public override void _Process(double delta)
    {
        this.timeSinceLastUse += delta;
    }

    public void UseCheck(bool hasTarget)
    {
        if (hasTarget && this.timeSinceLastUse >= 1 / this.cooldown)
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
        this.boss.Erebus();
        this.timeSinceLastUse = 0;
    }

}