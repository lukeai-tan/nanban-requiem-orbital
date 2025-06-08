using System;
using Godot;

public partial class RocketSamurai : BasicRangedEnemy
{
    public new void Initialize()
    {
        this.health = 200;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 75;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 50;
        this.attackSpeed = 0.5;
        this.blockCount = 1;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 50;
        this.projectileSpeed = 200;
        this.projectileScene = (PackedScene)GD.Load("res://path/to/Projectile.tscn");
        this.targeting = new TowerClosestToSelf(this);
        this.range = (EnemyDetectionRange)((PackedScene)GD.Load("res://path/to/EnemyDetectionRange.tscn")).Instantiate();
        base.Initialize();
    }

    public override string ToString()
    {
        return "Rocket Samurai: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}