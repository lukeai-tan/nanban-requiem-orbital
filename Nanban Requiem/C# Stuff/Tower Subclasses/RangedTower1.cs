using System;
using Godot;

public partial class RangedTower1 : RangedTowerBase
{
    public override void _Ready()
    {
        this.health = 1000;
        this.physDefense = 20;
        this.artsDefense = 20;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 10;
        this.attackSpeed = 3;
        this.projectileSpeed = 300;
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower 1: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}