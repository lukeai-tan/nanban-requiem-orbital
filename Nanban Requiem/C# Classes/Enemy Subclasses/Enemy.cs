using System;
using System.Threading;
using Godot;

// Layer 0 enemy logic that handles path following and hit response
// Required fields: health, physDefense, artsDefense, movementSpeed
public abstract partial class Enemy : CharacterBody2D, IUnit
{

    [Signal]
    public delegate void DamageBaseEventHandler(float damage);
    public event EventHandler Despawning;
    protected int health;
    protected int physDefense;
    protected int artsDefense;
    protected int movementSpeed;
    protected bool targetable = true;
    protected IPathing pathing;
    protected bool initialized = false;
    protected TextureProgressBar healthBar;
    protected AnimatedSprite2D animation;

    public override void _Ready()
    {
        this.healthBar = GD.Load<PackedScene>("res://Scenes/HealthBar/HealthBar.tscn").Instantiate<TextureProgressBar>();
        this.AddChild(this.healthBar);
        this.healthBar.MaxValue = this.health;
        this.healthBar.Value = this.health;
        this.animation = this.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public virtual void Initialize(Path2D path)
    {
        this.pathing = new BasicEnemyPathing(this);
        this.pathing.InitializePath(path);
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
            this.healthBar.Value = this.health;
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

    protected void ChangePath(Path2D path)
    {
        this.pathing.InitializePath(path);
    }

    public void ReachedBase(object pathing, EventArgs e)
    {
        this.EmitSignal(nameof(DamageBase), 1);
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
