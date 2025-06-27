using System;
using Godot;

public partial class RedHood : BasicMeleeEnemy
{

    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        base._Ready();
    }

}
