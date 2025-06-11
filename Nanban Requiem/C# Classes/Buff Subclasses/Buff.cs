using System;
using Godot;

public abstract partial class Buff : Node2D
{

    public event EventHandler Expired;
    [Export] protected int id = 0;
    [Export] protected double duration;
    [Export] protected double modifier;
    protected bool activated = false;

    public override void _Process(double delta)
    {
        if (this.activated)
        {
            if (this.duration <= 0)
            {
                this.Expired?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                this.duration -= delta;
            }
        }
    }

    public int GetId()
    {
        return this.id;
    }

    public double GetDuration()
    {
        return this.duration;
    }

    public abstract void Activate(IBuffable target);

    public abstract void Deactivate();

    public override string ToString()
    {
        return "Buff: " + this.modifier.ToString();
    }

}