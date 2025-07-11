using System;
using Godot;

public partial class Astrape : BossSkill
{
    
    protected Prts boss;

    public override void _Ready()
    {
        this.boss = this.GetParentOrNull<Prts>();
        this.boss.Half += (object boss, EventArgs e) => this.Execute();
    }

    public override void Execute()
    {
        this.boss.Astrape();
    }

}