using System;
using Godot;

public partial class ChickenTowerProjectile : TowerProjectileA
{
    private float timeSinceSpawn = 0f;
    private float timeToLand = 2f;


    public override void _Ready()
    {
        AnimatedSprite2D animation = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        animation.Visible = false;
        base._Ready();
    }


    public override void _PhysicsProcess(double delta)
    {
        if (!this.initialized || this.target == null)
        {
            return;
        }
        this.GlobalPosition = this.target.GlobalPosition;
        AnimatedSprite2D animation = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        animation.Visible = true;
        timeSinceSpawn += (float)delta;
        if (timeSinceSpawn >= timeToLand)
        {
            this.Land();
        }
    }
}