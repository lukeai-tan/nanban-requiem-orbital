using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and ranged attacking
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene, attackSpeed, range, targeting
public abstract partial class RangedTowerBase : Tower
{

    protected int rangedDamage;
    protected Attack rangedAttack;
    protected int projectileSpeed;
    protected BasicRangedAttack basicRanged;
    [Export] protected PackedScene projectileScene;
    protected double attackSpeed;
    protected double timeSinceLastAttack = 0;
    protected TowerDetectionRange range;
    protected ITargeting<Enemy> targeting;

    public override void _Ready()
    {
        this.range = this.GetNodeOrNull<TowerDetectionRange>("Detection Range");
        this.basicRanged = new BasicRangedAttack(this.projectileScene, this);
        this.basicRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
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
                this.basicRanged.SetModifiers(this.rangedDamage, 1);
                this.basicRanged.Execute(target);
            }
        }
    }

    public override void _ExitTree()
    {
        this.range.QueueFree();
        base._ExitTree();
    }

    public override string ToString()
    {
        return "Ranged Attack: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}