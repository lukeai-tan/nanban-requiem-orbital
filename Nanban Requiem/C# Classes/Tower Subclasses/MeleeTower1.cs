using System;
using Godot;

public partial class MeleeTower1 : MeleeTowerBase
{
    public override void _Ready()
    {
        this.health = 1000;
        this.physDefense = 20;
        this.artsDefense = 20;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 30;
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
