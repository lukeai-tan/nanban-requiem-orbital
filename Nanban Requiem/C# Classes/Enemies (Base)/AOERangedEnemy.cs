using System;
using Godot;

public abstract partial class AOERangedEnemy : BasicRangedEnemy
{

    [Export] protected PackedScene aoeScene;
    [Export] protected PackedScene buffScene;
    protected AOERangedBuff rangedBuff;

    public override void SetActions()
    {
        base.SetActions();
        this.rangedBuff = new AOERangedBuff(this.projectileScene, this, this.aoeScene, this.buffScene);
        this.rangedBuff.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
    }

    public override void Act()
    {
        if (this.timeSinceLastAttack >= 1 / this.attackSpeed)
        {
            Tower target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                this.Signal(target);
                this.rangedBuff.SetModifiers(this.attack * 2, this.atkModifier);
                this.rangedBuff.Execute(target);
                this.timeSinceLastAttack = 0;
            }
        }
    }

}