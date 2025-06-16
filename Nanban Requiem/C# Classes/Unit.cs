using System;
using System.Collections.Generic;
using Godot;

public abstract partial class Unit : CharacterBody2D, IUnit, IBuffable
{
    public event EventHandler Despawning;
    [Export] protected int health;
    protected int maxHealth;
    [Export] protected int physDefense;
    protected double pdModifier = 1;
    [Export] protected int artsDefense;
    protected double adModifier = 1;
    protected bool targetable = true;
    protected TextureProgressBar healthBar;
    protected Dictionary<int, Buff> status = new Dictionary<int, Buff>(30);

    public override void _Ready()
    {
        this.maxHealth = this.health;
        this.healthBar = GD.Load<PackedScene>("res://Scenes/HealthBar/HealthBar.tscn").Instantiate<TextureProgressBar>();
        this.AddChild(this.healthBar);
        this.healthBar.MaxValue = this.maxHealth;
        this.healthBar.Value = this.health;
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

    public void Heal(int heal)
    {
        int trueHealth = this.health + heal;
        if (trueHealth >= this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        else
        {
            this.health = trueHealth;
        }
        this.healthBar.Value = this.health;
    }

    public void TakePhysicalDamage(int damage)
    {
        double modifier = this.pdModifier < 0 ? 0 : this.pdModifier;
        this.TakeDamage(damage - (int)Math.Floor(this.physDefense * modifier));
    }

    public void TakeArtsDamage(int damage)
    {
        double modifier = this.adModifier < 0 ? 0 : this.adModifier;
        this.TakeDamage(damage - (int)Math.Floor(this.artsDefense * modifier));
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
            buff.Expired += this.ExpiredBuff;
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

    public void Despawn()
    {
        this.Despawning?.Invoke(this, EventArgs.Empty);
        this.QueueFree();
    }

    public int GetHealth()
    {
        return this.health;
    }
    
    public override string ToString()
    {
        return "HP: " + this.health.ToString();
    }

}