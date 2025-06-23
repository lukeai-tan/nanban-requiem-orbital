using System;
using Godot;

public partial class UIWipe : BossSkill
{
    
    protected Priestess boss;

    public override void _Ready()
    {
        this.boss = this.GetParentOrNull<Priestess>();
        this.boss.ThreeQ += (object boss, EventArgs e) => this.Execute();
        this.boss.OneQ += (object boss, EventArgs e) => this.Execute();
    }

    public override void Execute()
    {
        this.boss.UIWipe();
    }

}