using System;
using System.Runtime.CompilerServices;
using Godot;

// Layer 1 enemy logic that handles blocking and melee attacking
// Required fields: health, physDefense, artsDefense, movementSpeed, meleeDamage, meleeAttack, attackSpeed, blockCount
public abstract partial class BasicMeleeEnemy : Enemy, IAct, IBlock
{

    protected int meleeDamage;
    protected Attack meleeAttack;
    protected BasicMeleeAttack basicMelee;
    protected double attackSpeed;
    protected double timeSinceLastAttack = 0;
    protected int blockCount;
    protected Tower blocked = null;

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
        if (this.initialized)
        {
            if (this.IsBlocked())
            {
                this.Act();
            }
            else
            {
                this.animation.Play("running");
                this.pathing.Update(delta);
            }
            this.timeSinceLastAttack += delta;
        }
    }

    public virtual void Act()
    {
        if (this.timeSinceLastAttack >= 1f / this.attackSpeed)
        {
            this.animation.Play("attack");
            this.basicMelee.SetModifiers(this.meleeDamage, 1);
            this.basicMelee.Execute(this.blocked);
        }
    }

    public int GetBlockCount()
    {
        return this.blockCount;
    }

    public bool IsBlocked()
    {
        return this.blocked != null;
    }

    public void Blocked(Tower blocker)
    {
        this.blockCount = 0;
        this.blocked.Despawning += this.Unblocked;
    }

    public void Unblocked(object blocker, EventArgs e)
    {
        this.blocked = null;
    }

    public override string ToString()
    {
        return "Melee: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}