using System;
using Godot;

public partial class Hyades : BossSkill
{
    
    protected Priestess boss;

    public override void _Ready()
    {
        this.boss = this.GetParentOrNull<Priestess>();
        this.boss.Half += (object boss, EventArgs e) => this.Execute();
    }

    public override void Execute()
    {
        this.boss.Hyades();
    }

}