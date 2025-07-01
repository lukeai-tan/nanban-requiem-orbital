using System;
using Godot;

public partial class Inferno : BossSkill
{

    protected Priestess boss;
    protected bool hasTargets = false;

    public override void _Ready()
    {
        this.priority = 0;
        this.boss = this.GetParentOrNull<Priestess>();
        this.boss.HasTower += (object boss, BoolEventArgs e) => this.UseCheck(e.boolean);
    }

    public void UseCheck(bool hasTarget)
    {
        if (hasTarget)
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
        this.boss.Inferno();
    }

}