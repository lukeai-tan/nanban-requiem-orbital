using System;
using Godot;

public partial class PhysicalDOT : DamageOverTime
{
    public override void _Ready()
    {
        this.attack = new PhysicalAttack();
    }
}