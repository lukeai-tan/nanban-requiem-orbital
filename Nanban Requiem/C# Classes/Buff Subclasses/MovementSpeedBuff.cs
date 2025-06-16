using System;
using Godot;

public partial class MovementSpeedBuff : Buff
{

    protected Enemy target = null;

    public override void Activate(IBuffable target)
    {
        if (target is Enemy unit)
        {
            this.target = unit;
            unit.ModifyMovementSpeed(this.modifier);
        }
        this.activated = true;
    }

    public override void Deactivate()
    {
        if (this.activated && this.target != null)
        {
            this.target.ModifyMovementSpeed(-this.modifier);
        }
        base.Deactivate();
    }

    public override string ToString()
    {
        return "Movement Speed " + base.ToString();
    }

}