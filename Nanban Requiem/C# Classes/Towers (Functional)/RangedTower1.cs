using System;
using Godot;

public partial class RangedTower1 : RangedTowerBase
{
    public override void _Ready()
    {
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower 1: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}