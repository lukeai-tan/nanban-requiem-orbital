using System;
using Godot;

public partial class FlamingProjectile : TowerProjectileAA
{
    public override void _Ready()
    {
        AnimatedSprite2D animation = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        if (animation != null)
        {
            animation.Play("default");
        }
    }
}
