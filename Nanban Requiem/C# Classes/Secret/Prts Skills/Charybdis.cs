using System;
using Godot;

public partial class Charybdis : BossSkill
{
    
    protected Prts boss;
    [Export] protected double cooldown;
    protected double timeSinceLastUse = 0;

    public override void _Ready()
    {
        this.priority = 1;
        this.boss = this.GetParentOrNull<Prts>();
        this.boss.HasTower += (object boss, BoolEventArgs e) => this.UseCheck(e.boolean);
        this.boss.Active += (object boss, EventArgs e) => this.timeSinceLastUse = 5;
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
        this.boss.Charybdis();
        this.timeSinceLastUse = 0;
    }

}