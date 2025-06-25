using System;
using Godot;

public partial class RocketSamurai : BasicRangedEnemy
{
    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new TowerClosestToSelf(this);
        this.Scale = new Vector2(2f, 2f);
        this.Position = new Vector2(0, -25);
        base._Ready();
    }

    public override string ToString()
    {
        return "Rocket Samurai: " + this.attack.ToString() + " " + base.ToString();
    }

}