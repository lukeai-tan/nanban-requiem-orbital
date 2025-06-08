using System;
using Godot;

public partial class RangedTower1 : RangedTowerBase
{
    public new void Initialize()
    {
        this.health = 1000;
        this.physDefense = 20;
        this.artsDefense = 20;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 10;
        this.attackSpeed = 3;
        this.projectileSpeed = 300;
        this.projectileScene = (PackedScene)GD.Load("res://path/to/Projectile.tscn");
        this.targeting = new EnemyClosestToBase();
        this.range = (TowerDetectionRange)((PackedScene)GD.Load("res://path/to/TowerDetectionRange.tscn")).Instantiate();
        base.Initialize();
    }

    public override string ToString()
    {
        return "Ranged Tower 1: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}