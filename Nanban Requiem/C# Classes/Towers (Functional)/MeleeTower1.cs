using System;
using Godot;

public partial class MeleeTower1 : MeleeTowerBase
{
    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        this.targeting = new EnemyWithMostHealth();
        base._Ready();
    }

    public override string ToString()
    {
        return "Melee Tower 1: " + this.attack.ToString() + " " + base.ToString();
    }

}
