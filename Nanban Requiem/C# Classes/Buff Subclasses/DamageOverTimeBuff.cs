using System;
using Godot;

public abstract partial class DamageOverTimeBuff : Buff
{

    protected Unit target = null;
    protected Attack attack;
    [Export] protected int damage;
    [Export] protected double interval;
    protected double timeSinceLastAttack = 0;

    public override void _Process(double delta)
    {
        if (this.activated)
        {
            if (this.duration <= 0)
            {
                this.IsExpired();
            }
            else
            {
                if (target != null && timeSinceLastAttack >= this.interval)
                {
                    if (this.animation != null)
                    {
                        this.animation.Play("effect");
                    }
                    this.attack.Hit(this.target, this.damage, this.modifier);
                    this.timeSinceLastAttack = 0;
                }
                this.duration -= delta;
                this.timeSinceLastAttack += delta;
            }
        }
    }

    public override void Activate(IBuffable target)
    {
        if (target is Unit unit)
        {
            this.target = unit;
        }
        this.activated = true;
    }

    public override string ToString()
    {
        return "DOT " + base.ToString();
    }

}