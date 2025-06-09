using System;
using Godot;

public partial class RangedTower2 : AOETowerBase
{
    public override void _Ready()
    {
        this.health = 800;
        this.physDefense = 10;
        this.artsDefense = 10;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 20;
        this.attackSpeed = 1;
        this.projectileSpeed = 300;
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower 2: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}