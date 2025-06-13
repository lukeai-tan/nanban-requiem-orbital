using System;
using Godot;

public class BasicMeleeAttack : Action
{

    protected int damage = 0;
    protected double multiplier = 1;
    protected Attack attack;

    public BasicMeleeAttack()
    {
        this.priority = 1;
    }

    public void SetAttack(Attack attack)
    {
        this.attack = attack;
        this.usable = true;
    }

    public void SetModifiers(int damage, double multiplier)
    {
        this.damage = damage;
        this.multiplier = multiplier;
    }

    public override void Execute<T>(T target)
    {
        if (this.IsUsable())
        {
            this.attack.Hit(target, this.damage, this.multiplier);
        }
    }

    public override string ToString()
    {
        return "Basic Melee: " + this.attack.ToString() + " " + base.ToString();
    }

}