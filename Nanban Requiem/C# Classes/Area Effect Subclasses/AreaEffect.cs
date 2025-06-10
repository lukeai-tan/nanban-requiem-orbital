using System;
using System.Threading;
using Godot;

// Base class that performs a certain action to all targets in range
public abstract partial class AreaEffect<T> : Area2D, IAreaEffect
    where T : Node2D, IUnit
{

    protected bool active = false;
    protected Action effect;

    public void Activate(Vector2 position, Action effect)
    {
        this.GlobalPosition = position;
        this.effect = effect;
        this.active = true;
    }

    public void Effect()
    {
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