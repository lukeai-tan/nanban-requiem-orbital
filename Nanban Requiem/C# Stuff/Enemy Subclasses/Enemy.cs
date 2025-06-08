using System;
using System.Threading;
using Godot;

// Layer 0 enemy logic that handles path following and hit response
// Required fields: health, physDefense, artsDefense, movementSpeed
public abstract partial class Enemy : CharacterBody2D, IUnit
{

    public event EventHandler Despawning;
    protected int health;
    protected int physDefense;
    protected int artsDefense;
    protected int movementSpeed;
    protected bool targetable = true;
    protected IPathing pathing;
    protected bool initialized = false;

    protected virtual void Initialize()
    {
        this.pathing = new BasicEnemyPathing(this.movementSpeed, this);
        this.pathing.PathCompletion += this.ReachedBase;
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

    public float GetProgress()
    {
        return this.pathing.GetProgress();
    }

    public int GetHealth()
    {
        return this.health;
    }

    public void InitializePath(Path2D path)
    {
        this.pathing.InitializePath(path);
    }

    public void ReachedBase(object pathing, EventArgs e)
    {
        this.QueueFree();
    }

    public override void _ExitTree()
    {
        this.pathing.PathCompletion -= this.ReachedBase;
    }
    public override string ToString()
    {
        return "HP: " + this.health.ToString();
    }

}
