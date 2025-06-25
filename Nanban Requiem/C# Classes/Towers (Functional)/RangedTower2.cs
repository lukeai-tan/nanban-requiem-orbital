using System;
using Godot;

public partial class RangedTower2 : AOETowerBase
{
    public override void _Ready()
    {
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower 2: " + this.attack.ToString() + " " + base.ToString();
    }

}