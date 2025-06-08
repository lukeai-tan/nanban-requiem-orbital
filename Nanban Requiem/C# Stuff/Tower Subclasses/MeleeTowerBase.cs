using System;
using Godot;

// Layer 2 tower logic that handles melee attacking and targeting
// Required fields: health, physDefense, artsDefense, meleeDamage, meleeAttack, attackSpeed, blockCount, range, targeting
public abstract partial class MeleeTowerBase : ObstacleBase
{

    protected int meleeDamage;
    protected Attack meleeAttack;
    protected BasicMeleeAttack basicMelee;
    protected double attackSpeed;
    protected double timeSinceLastAttack = 0;
    protected ITargeting<Enemy> targeting;

    protected override void Initialize()
    {
        this.basicMelee = new BasicMeleeAttack();
        this.basicMelee.SetAttack(this.meleeAttack);
        base.Initialize();
    }

    public override void _Process(double delta)
    {
        if (this.initialized)
        {
            this.Act();
            this.timeSinceLastAttack += delta;
        }
    }

    public virtual void Act()
    {
        if (this.timeSinceLastAttack >= 1f / this.attackSpeed)
        {
            Enemy target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                this.basicMelee.SetModifiers(this.meleeDamage, 1);
                this.basicMelee.Execute(target);
            }
        }
    }

    public override string ToString()
    {
        return "Melee Attack: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}