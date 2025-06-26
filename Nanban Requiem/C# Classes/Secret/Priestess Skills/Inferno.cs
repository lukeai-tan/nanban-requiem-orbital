using System;
using Godot;

public partial class Inferno : BossSkill
{

    protected Priestess boss;

    public override void _Ready()
    {
        this.priority = 0;
        this.usable = true;
        this.boss = this.GetParentOrNull<Priestess>();
    }

    public override void Execute()
    {
        this.boss.Inferno();
    }

}