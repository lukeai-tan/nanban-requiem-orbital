using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using Godot;

// Layer 0 enemy logic that handles path following and hit response
// Required fields: health, physDefense, artsDefense, movementSpeed
public abstract partial class Enemy : Unit
{

    [Signal]
    public delegate void DamageBaseEventHandler(float damage);
    [Export] protected int movementSpeed;
    protected double msModifier = 1;
    protected IPathing pathing;
    protected bool initialized = false;
    protected AnimatedSprite2D animation;

    public override void _Ready()
    {
        base._Ready();
        this.animation = this.GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public virtual void Initialize(Path2D path)
    {
        BasicEnemyPathing pathing = new BasicEnemyPathing(this);
        pathing.InitializePath(path);
        this.pathing = pathing;
        this.pathing.PathCompletion += this.ReachedObjective;
        this.initialized = true;
    }

    public virtual void Move(double delta)
    {
        double modifier = this.msModifier < 0 ? 0 : this.msModifier;
        this.pathing.Update(this.movementSpeed * (float) modifier * (float) delta);
    }

    public virtual void ModifyMovementSpeed(double multiplier)
    {
        this.msModifier += multiplier;
    }
    
    public virtual float GetProgress()
    {
        return this.pathing.GetProgress();
    }

    protected void ChangePath(Path2D path)
    {
        if (this.pathing is BasicEnemyPathing pathing)
        {
            pathing.InitializePath(path);
        }
    }

    public virtual void ReachedObjective(object pathing, EventArgs e)
    {
        this.EmitSignal(nameof(DamageBase), 1);
        this.Despawn();
    }

    public override void _ExitTree()
    {
        this.pathing.PathCompletion -= this.ReachedObjective;
    }

}
