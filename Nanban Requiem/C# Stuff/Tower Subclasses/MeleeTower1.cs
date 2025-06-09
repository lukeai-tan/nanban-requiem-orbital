using System;
using Godot;

public partial class MeleeTower1 : MeleeTowerBase
{
    public override void _Ready()
    {
        this.health = 1200;
        this.physDefense = 50;
        this.artsDefense = 30;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 50;
        this.attackSpeed = 1.5;
        this.blockCount = 2;
        this.targeting = new EnemyWithMostHealth();
        base._Ready();
    }

    public override string ToString()
    {
        return "Melee Tower 1: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}
