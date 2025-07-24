using System;
using Godot;

public partial class DemonSlime : BasicRangedEnemy
{
    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        this.rangedAttack = new ArtsAttack();
        this.targeting = new TowerClosestToSelf(this);
        base._Ready();
    }

    public override void ReachedObjective(object pathing, EventArgs e)
    {
        this.EmitSignal(nameof(DamageBase), 5);
        this.Despawn();
    }

}