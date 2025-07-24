using System;
using Godot;

public partial class AttackBuff : Buff
{

    protected Unit target = null;

    public override void Activate(IBuffable target)
    {
        if (target is Unit unit)
        {
            this.target = unit;
            unit.ModifyAtk(this.modifier);
        }
        this.activated = true;
    }

    public override void Deactivate()
    {
        if (this.activated && this.target != null)
        {
            this.target.ModifyAtk(-this.modifier);
        }
        base.Deactivate();
    }

    public override string ToString()
    {
        return "Attack Buff " + base.ToString();
    }

}