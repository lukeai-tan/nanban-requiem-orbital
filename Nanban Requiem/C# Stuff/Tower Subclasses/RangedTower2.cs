using System;
using Godot;

public partial class RangedTower2 : AOETowerBase
{
    public new void Initialize()
    {
        this.health = 800;
        this.physDefense = 10;
        this.artsDefense = 10;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 20;
        this.attackSpeed = 1;
        this.projectileSpeed = 300;
        this.projectileScene = (PackedScene)GD.Load("res://path/to/AOEProjectile.tscn");
        this.targeting = new EnemyClosestToBase();
        this.range = (TowerDetectionRange)((PackedScene)GD.Load("res://path/to/TowerDetectionRange.tscn")).Instantiate();
        this.areaEffect = (TowerSingleUse)((PackedScene)GD.Load("res://path/to/TowerSingleUse.tscn")).Instantiate();
        base.Initialize();
    }

    public override string ToString()
    {
        return "Ranged Tower 2: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}