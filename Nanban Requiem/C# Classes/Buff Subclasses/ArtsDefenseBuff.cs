using System;
using Godot;

public partial class ArtsDefenseBuff : Buff
{

    protected Unit target = null;

    public override void Activate(IBuffable target)
    {
        if (target is Unit unit)
        {
            this.target = unit;
            unit.ModifyArtsDefense(this.modifier);
        }
        this.activated = true;
    }

    public override void Deactivate()
    {
        if (this.activated && this.target != null)
        {
            this.target.ModifyArtsDefense(-this.modifier);
        }
        base.Deactivate();
    }

    public override string ToString()
    {
        return "Physical Defense " + base.ToString();
    }

}