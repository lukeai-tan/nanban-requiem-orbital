using System;
using Godot;

// Layer 0 tower logic that handles hit response
// Required fields: health, physDefense, artsDefense
public abstract partial class Tower : CharacterBody2D, IUnit
{

    public event EventHandler Despawning;
    protected int health;
    protected int physDefense;
    protected int artsDefense;
    protected bool targetable = true;
    protected bool initialized = false;

    protected virtual void Initialize()
    {
        this.initialized = true;
    }

    public bool CanTarget()
    {
        return this.targetable;
    }

    protected virtual void TakeDamage(int damage)
    {
        int trueDamage = damage > 1 ? damage : 1;
        if (this.health <= trueDamage)
        {
            this.Despawn();
        }
        else
        {
            this.health -= trueDamage;
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        this.TakeDamage(damage - this.physDefense);
    }

    public void TakeArtsDamage(int damage)
    {
        this.TakeDamage(damage - this.artsDefense);
    }

    public void Despawn()
    {
        this.Despawning?.Invoke(this, EventArgs.Empty);
        this.QueueFree();
    }

    public override string ToString()
    {
        return "HP: " + this.health.ToString();
    }

}