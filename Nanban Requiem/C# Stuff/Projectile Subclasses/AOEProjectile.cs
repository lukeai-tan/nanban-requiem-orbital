using System;
using Godot;

public abstract partial class AOEProjectile<T> : Projectile<T>
    where T : Node2D, IUnit
{
    protected PackedScene areaEffectScene;

    public void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position, PackedScene areaEffectScene)
    {
        this.areaEffectScene = areaEffectScene;
        base.Initialize(attack, damage, multiplier, target, speed, position);
    }

    public override void Initialize(Attack attack, int damage, double multipler, T target, int speed, Vector2 position)
    {
        GD.PushWarning("AOEProjectile requires an AreaEffect.");
    }

    protected override void Land()
    {
        BasicMeleeAttack hit = new BasicMeleeAttack();
        hit.SetAttack(this.attack);
        hit.SetModifiers(this.damage, this.multiplier);
        Node areaEffect = this.areaEffectScene.Instantiate();
        if (areaEffect is AreaEffect<T> effect)
        {
            this.GetParent().AddChild(effect);
            effect.Activate(this.GlobalPosition, hit);
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "AOE " + base.ToString();
    }

}