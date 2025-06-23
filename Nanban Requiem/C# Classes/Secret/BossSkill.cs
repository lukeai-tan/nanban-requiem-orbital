using System;
using Godot;

public abstract partial class BossSkill : Node2D
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

    public abstract void Execute();

    public int GetPriority()
    {
        return this.priority;
    }
 
}