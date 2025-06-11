using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using Godot;

// Layer 0 enemy logic that handles path following and hit response
// Required fields: health, physDefense, artsDefense, movementSpeed
public abstract partial class Enemy : CharacterBody2D, IUnit, IBuffable
{

    [Signal]
    public delegate void DamageBaseEventHandler(float damage);
    public event EventHandler Despawning;
    [Export] protected int health;
    [Export] protected int physDefense;
    protected double pdModifier = 1;
    [Export] protected int artsDefense;
    protected double adModifier = 1;
    [Export] protected int movementSpeed;
    protected double msModifier = 1;
    protected bool targetable = true;
    protected IPathing pathing;
    protected bool initialized = false;
    protected TextureProgressBar healthBar;
    protected AnimatedSprite2D animation;
    protected Dictionary<int, Buff> status = new Dictionary<int, Buff>(30);

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

    public virtual void Move(double delta)
    {
        double modifier = this.msModifier < 0 ? 0 : this.msModifier;
        this.pathing.Update(this.movementSpeed * (float) modifier * (float) delta);
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
        double modifier = this.pdModifier < 0 ? 0 : this.pdModifier;
        this.TakeDamage(damage - (int) Math.Floor(this.physDefense * modifier));
    }

    public void TakeArtsDamage(int damage)
    {
        double modifier = this.adModifier < 0 ? 0 : this.adModifier;
        this.TakeDamage(damage - (int) Math.Floor(this.artsDefense * modifier));
    }

    public void ReceiveBuff(Buff buff)
    {
        int id = buff.GetId();
        if (id <= 0 || id >= 30)
        {
            buff.QueueFree();
        }
        else if (this.status.ContainsKey(id))
        {
            Buff old = this.status[id];
            if (old.GetDuration() <= buff.GetDuration())
            {
                this.ClearBuff(old);
                this.status[id] = buff;
                this.AddChild(buff);
                buff.Activate(this);
                buff.Expired += this.ExpiredBuff;
            }
            else
            {
                buff.QueueFree();
            }
        }
        else
        {
            this.status[id] = buff;
            this.AddChild(buff);
            buff.Activate(this);
        }
    }

    protected void ExpiredBuff(object expired, EventArgs e)
    {
        this.ClearBuff((Buff)expired);
    }

    protected void ClearBuff(Buff buff)
    {
        int id = buff.GetId();
        this.status.Remove(id);
        buff.Deactivate();
    }

    public void ModifyPhysicalDefense(double multiplier)
    {
        this.pdModifier += multiplier;
    }

    public void ModifyArtsDefense(double multiplier)
    {
        this.adModifier += multiplier;
    }

    public void ModifyMovementSpeed(double multiplier)
    {
        this.msModifier += multiplier;
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
        this.Despawn();
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
