using System;
using System.Linq;
using Godot;

public partial class TowerManipulation : Buff
{

    protected Tower victim = null;
    protected int damage;
    protected BasicRangedAttack rangedCounter;
    [Export] protected PackedScene projectileScene;
    protected GlobalDetectionRange range;
    protected PrtsControlled targeting;

    public override void _Ready()
    {
        base._Ready();
        this.range = this.GetNodeOrNull<GlobalDetectionRange>("Range");
    }

    protected void Fire(object victim, EventArgs e)
    {
        if (this.activated)
        {
            Tower target = this.targeting.GetTarget(this.range.GetAllTowers());
            if (target != null)
            {
                this.rangedCounter.SetModifiers(this.damage, 1);
                this.rangedCounter.Execute(target);
            }
        }
    }

    public override void Activate(IBuffable target)
    {
        if (target is Tower tower)
        {
            this.damage = (int)tower.GetAttack();
            tower.ModifyAtk(this.modifier);
            this.victim = tower;
            this.targeting = new PrtsControlled(this.victim);
            this.rangedCounter = new BasicRangedAttack(this.projectileScene, this.victim);
            this.rangedCounter.SetAttackAndSpeed(new ArtsAttack(), 300);
            if (this.victim is IAct possessed)
            {
                possessed.TakeAction += this.Fire;
            }
        }
        this.activated = true;
    }

    public override void Deactivate()
    {
        if (this.activated && this.victim != null)
        {
            this.victim.ModifyAtk(-this.modifier);
        }
        if (this.victim is IAct possessed)
        {
            possessed.TakeAction -= this.Fire;
        }
        base.Deactivate();
    }

}