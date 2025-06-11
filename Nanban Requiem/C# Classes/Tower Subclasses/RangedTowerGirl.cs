using System;
using Godot;

public partial class RangedTowerGirl : RangedBufferBase
{
    public override void _Ready()
    {
        this.health = 500;
        this.physDefense = 10;
        this.artsDefense = 10;
        this.rangedAttack = new ArtsAttack();
        this.rangedDamage = 50;
        this.attackSpeed = 0.5;
        this.projectileSpeed = 300;
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
        return "Ranged Tower Girl: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}