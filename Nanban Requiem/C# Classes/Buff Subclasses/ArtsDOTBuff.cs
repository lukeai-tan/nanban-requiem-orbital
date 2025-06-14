using System;
using Godot;

public partial class ArtsDOTBuff : DamageOverTimeBuff
{
    public override void Activate(IBuffable target)
    {
        this.attack = new ArtsAttack();
        base.Activate(target);
    }

}