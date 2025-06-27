using System;
using Godot;

public partial class Nightborne : AOERangedEnemy
{

    public override void _Ready()
    {
        this.meleeAttack = new ArtsAttack();
        this.rangedAttack = new ArtsAttack();
        this.targeting = new TowerClosestToSelf(this);
        base._Ready();
    }

}