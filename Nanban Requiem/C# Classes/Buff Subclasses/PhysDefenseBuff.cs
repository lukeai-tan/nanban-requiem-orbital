using System;
using Godot;

public partial class PhysDefenseBuff : Buff
{

    protected IUnit target = null;

    public PhysDefenseBuff(double modifier, double duration)
    {
        this.modifier = modifier;
        this.duration = duration;
    }

    public override void Activate(IBuffable target)
    {
        if (target is IUnit unit)
        {
            this.target = unit;
            unit.ModifyPhysicalDefense(this.modifier);
        }
        this.activated = true;
    }

    public override void Deactivate()
    {
        if (this.activated && this.target != null)
        {
            this.target.ModifyPhysicalDefense(-this.modifier);
        }
        this.QueueFree();
    }

    public override string ToString()
    {
        return "Physical Defense " + base.ToString();
    }

}