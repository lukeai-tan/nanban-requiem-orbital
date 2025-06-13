using System;
using Godot;

public abstract partial class ProjectileA<T> : Projectile<T>
    where T : Unit
{
    
    protected Attack attack;
    protected int damage;
    protected double multiplier;

    public virtual void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position)
    {
        this.attack = attack;
        this.damage = damage;
        this.multiplier = multiplier;
        base.Initialize(target, speed, position);
    }

    protected override void Land()
    {
        this.attack.Hit(this.target, this.damage, this.multiplier);
        base.Land();
    }

    public override string ToString()
    {
        return "Attack " + base.ToString() + this.damage.ToString() + " x " + this.multiplier.ToString();
    }

}