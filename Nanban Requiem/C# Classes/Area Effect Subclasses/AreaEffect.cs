using System;
using System.Threading;
using Godot;

// Base class that performs a certain action to all targets in range
public abstract partial class AreaEffect<T> : Area2D, IAreaEffect
    where T : Unit
{

    protected bool active = false;
    protected Action effect;
    protected AnimatedSprite2D animation = null;

    public override void _Ready()
    {
        AnimatedSprite2D animation = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
        if (animation != null)
        {
            this.animation = animation;
        }
    }

    public void Activate(Vector2 position, Action effect)
    {
        this.GlobalPosition = position;
        this.effect = effect;
        this.active = true;
    }

    public void Effect()
    {
        if (this.animation != null)
        {
            this.animation.Play("effect");
        }
        Godot.Collections.Array<Node2D> bodies = this.GetOverlappingBodies();
        foreach (Node2D body in bodies)
        {
            if (body is T target)
            {
                this.effect.Execute(target);
            }
        }
    }

    public override string ToString()
    {
        return effect.ToString();
    }

}