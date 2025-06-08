using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using Godot;

public partial class Projectile<T> : CharacterBody2D
    where T : Node2D, IUnit
{

    protected Attack attack;
    protected int damage;
    protected double multiplier;
    protected T target;
    protected int speed;
    protected bool initialized = false;

    public virtual void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position)
    {
        this.attack = attack;
        this.damage = damage;
        this.multiplier = multiplier;
        this.speed = speed;
        this.target = target;
        this.target.Despawning += this.EnemyDespawn;
        this.GlobalPosition = position;
        this.initialized = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!this.initialized || this.target == null)
        {
            return;
        }

        Vector2 direction = this.target.GlobalPosition - this.GlobalPosition;
        this.Rotation = direction.Angle();
        this.Velocity = direction.Normalized() * this.speed;
        this.GlobalPosition += this.Velocity * (float)delta;

        if (this.GlobalPosition.DistanceTo(this.target.GlobalPosition) < 5f)
        {
            this.Land();
        }

    }

    protected virtual void Land()
    {
        this.attack.Hit(this.target, this.damage, this.multiplier);
        this.QueueFree();
    }

    protected virtual void EnemyDespawn(object target, EventArgs e)
    {
        this.QueueFree();
    }

    public override void _ExitTree()
    {
        this.target.Despawning -= EnemyDespawn;
        base._ExitTree();
    }
    public override string ToString()
    {
        return "Projectile: " + this.damage.ToString() + " x " + this.multiplier.ToString() + " to " + this.target.ToString();
    }

}