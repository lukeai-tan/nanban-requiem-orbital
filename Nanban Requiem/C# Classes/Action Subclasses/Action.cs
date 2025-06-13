using Godot;
using System;

// Base class for any action performable on a Unit, has a priority which is used in skill picking
public abstract class Action : IAction
{

    protected bool usable = false;
    protected int priority;

    public virtual bool IsUsable()
    {
        return this.usable;
    }

    public override string ToString()
    {
        return "Priority: " + this.priority.ToString();
    }

    public abstract void Execute<T>(T target)
        where T : Unit;

    public int GetPriority()
    {
        return this.priority;
    }

}