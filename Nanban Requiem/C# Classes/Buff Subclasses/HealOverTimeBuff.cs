using System;
using Godot;

public partial class HealOverTimeBuff : DamageOverTimeBuff
{
    public override void Activate(IBuffable target)
    {
        this.attack = new Heal();
        base.Activate(target);
    }

}