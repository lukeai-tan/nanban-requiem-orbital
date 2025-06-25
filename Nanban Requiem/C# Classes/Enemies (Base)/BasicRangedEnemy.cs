using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;

// Layer 2 enemy logic that handles specialized detection, targeting and ranged attacking
// Required fields: health, physDefense, artsDefense, movementSpeed, meleeDamage, meleeAttack, attackSpeed, blockCount,
// rangedDamage, rangedAttack, projectileSpeed, projectileScene, targeting, range
public abstract partial class BasicRangedEnemy : BasicMeleeEnemy
{

    protected Attack rangedAttack;
    [Export] protected int projectileSpeed;
    protected BasicRangedAttack basicRanged;
    [Export] protected PackedScene projectileScene;
    protected ITargeting<Tower> targeting;
    protected EnemyDetectionRange range;

    public override void _Ready()
    {
        this.range = this.GetNodeOrNull<EnemyDetectionRange>("DetectionRange");
        base._Ready();
    }

    public override void SetActions()
    {
        base.SetActions();
        this.basicRanged = new BasicRangedAttack(this.projectileScene, this);
        this.basicRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
    }

    public override void _Process(double delta)
    {
        if (this.initialized)
        {
            if (this.IsBlocked())
            {
                base.Act();
            }
            else
            {
                if (this.animation != null)
                {
                    this.animation.Play("running");
                }
                this.Act();
                this.Move(delta);
            }
            this.timeSinceLastAttack += delta;
        }
    }

    public override void Act()
    {
        if (this.timeSinceLastAttack >= 1 / this.attackSpeed)
        {
            Tower target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                this.basicRanged.SetModifiers(this.attack * 2, this.atkModifier);
                this.basicRanged.Execute(target);
                this.timeSinceLastAttack = 0;
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
        return "Ranged: " + this.attack.ToString() + " " + base.ToString();
    }

}