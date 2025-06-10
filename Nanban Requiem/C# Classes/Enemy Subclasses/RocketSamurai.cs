using System;
using Godot;

public partial class RocketSamurai : BasicRangedEnemy
{
    public override void _Ready()
    {
        this.health = 300;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 75;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 60;
        this.attackSpeed = 1;
        this.blockCount = 1;
        this.rangedAttack = new PhysicalAttack();
        this.rangedDamage = 100;
        this.projectileSpeed = 200;
        this.targeting = new TowerClosestToSelf(this);
        this.Scale = new Vector2(2f, 2f);
        this.Position = new Vector2(0, -25);
        base._Ready();
    }

    public override string ToString()
    {
        return "Rocket Samurai: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}