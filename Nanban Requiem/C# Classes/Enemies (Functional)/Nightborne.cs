using System;
using Godot;

public partial class Nightborne : BasicMeleeEnemy
{

    public override void _Ready()
    {
        this.meleeAttack = new ArtsAttack();
        base._Ready();
    }

    public override void ReachedObjective(object pathing, EventArgs e)
    {
        this.EmitSignal(nameof(DamageBase), 2);
        this.Despawn();
    }

}