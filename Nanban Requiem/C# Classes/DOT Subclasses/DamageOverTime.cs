using System;
using Godot;

public abstract partial class DamageOverTime : Node2D
{

    protected IUnit target;
    protected Attack attack;
    protected int damage;
    [Export] protected double multiplier;
    [Export] protected double duration;
    [Export] protected double interval;
    protected double timeSinceLastAttack = 0;
    protected bool initialized = false;

    public void Initialize(IUnit target, int damage)
    {
        this.target = target;
        this.target.Despawning += this.EnemyDespawn;
        this.damage = damage;
        this.initialized = true;
    }

    public override void _Process(double delta)
    {
        try
        {
            if (this.initialized)
            {
                if (this.duration < 0)
                {
                    this.QueueFree();
                }
                else if (timeSinceLastAttack >= this.interval)
                {
                    this.attack.Hit(this.target, this.damage, this.multiplier);
                    this.timeSinceLastAttack = 0;
                }
                this.duration -= delta;
                this.timeSinceLastAttack += delta;
            }
        }
        catch (Exception e)
        {
            GD.Print(e);
        }

    }

    protected virtual void EnemyDespawn(object target, EventArgs e)
    {
        this.QueueFree();
    }

    public override void _ExitTree()
    {
        this.target.Despawning -= this.EnemyDespawn;
        base._ExitTree();
    }

}