using System;
using Godot;

public partial class RangedTowerGirl1 : RangedBufferBase
{
    public override void _Ready()
    {
        this.rangedAttack = new ArtsAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override void _Process(double delta)
    {
        AnimatedSprite2D sprite = this.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        if (sprite != null)
        {
            sprite.Play("idle");
        }
        base._Process(delta);
    }

    public override string ToString()
    {
        return "Ranged Tower Girl 1: " + this.attack.ToString() + " " + base.ToString();
    }

}