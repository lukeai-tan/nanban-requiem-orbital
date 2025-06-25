using System;
using Godot;

// Layer 2 tower logic that handles melee attacking and targeting
// Required fields: health, physDefense, artsDefense, meleeDamage, meleeAttack, attackSpeed, blockCount, range, targeting
public abstract partial class MeleeTowerBase : ObstacleBase, IAct
{

    protected Attack meleeAttack;
    protected BasicMeleeAttack basicMelee;
    [Export] protected double attackSpeed;
    protected double timeSinceLastAttack = 0;
    protected ITargeting<Enemy> targeting;

    public override void _Ready()
    {
        this.SetActions();
        base._Ready();
    }

    public virtual void SetActions()
    {
        this.basicMelee = new BasicMeleeAttack();
        this.basicMelee.SetAttack(this.meleeAttack);
    }

    public override void _Process(double delta)
    {
        this.Act();
        this.timeSinceLastAttack += delta;
    }

    public virtual void Act()
    {
        if (this.timeSinceLastAttack >= 1f / this.attackSpeed)
        {
            Enemy target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                this.basicMelee.SetModifiers(this.attack, this.atkModifier);
                this.basicMelee.Execute(target);
                this.timeSinceLastAttack = 0;
            }
        }
    }

    public override string ToString()
    {
        return "Melee Attack: " + this.attack.ToString() + " " + base.ToString();
    }

}