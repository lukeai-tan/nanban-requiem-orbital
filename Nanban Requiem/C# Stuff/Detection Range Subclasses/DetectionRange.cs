using System;
using System.Collections.Generic;
using System.ComponentModel;
using Godot;

// Base class for detecting and tracking units
public abstract partial class DetectionRange<T> : Area2D
    where T : Node2D, IUnit
{

    protected List<T> targetsInRange = new List<T>();

    public override void _Ready()
    {
        this.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        this.Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
    }

    protected void OnBodyEntered(Node body)
    {
        if (body is T unit)
        {
            this.OnUnitEntered(unit);
        }
    }

    protected void OnBodyExited(Node body)
    {
        if (body is T unit)
        {
            this.OnUnitExited(unit);
        }
    }

    protected virtual void OnUnitEntered(T unit)
    {
        this.targetsInRange.Add(unit);
        unit.Despawning += this.UnitDespawned;
    }

    protected void UnitDespawned(object unit, EventArgs e)
    {
        this.OnUnitExited((T)unit);
    }

    protected virtual void OnUnitExited(T unit)
    {
        this.targetsInRange.Remove(unit);
    }

    public List<T> GetTargets()
    {
        return this.targetsInRange;
    }

    public override string ToString()
    {
        return targetsInRange.ToString();
    }

    public override void _ExitTree()
    {
        foreach (T unit in this.targetsInRange)
        {
            unit.Despawning -= this.UnitDespawned;
        }
        base._ExitTree();
    }

}