using System;
using Godot;

public partial class PhysDefenseBuff : Buff
{

    protected Unit target = null;

    public override void Activate(IBuffable target)
    {
        if (target is Unit unit)
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
        base.Deactivate();
    }

    public override string ToString()
    {
        return "Physical Defense " + base.ToString();
    }

}