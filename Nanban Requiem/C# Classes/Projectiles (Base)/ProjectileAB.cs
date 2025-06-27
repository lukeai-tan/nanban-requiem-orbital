using System;
using Godot;

public partial class ProjectileAB<T> : ProjectileAA<T>
    where T : Unit
{

    protected PackedScene buffScene;

    public void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position, PackedScene areaEffectScene, PackedScene buffScene)
    {
        this.buffScene = buffScene;
        base.Initialize(attack, damage, multiplier, target, speed, position, areaEffectScene);
    }

    public override void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position, PackedScene areaEffectScene)
    {
        GD.PushWarning("AOEBuffProjectile requires a BuffScene.");
    }

    public override void Initialize(Attack attack, int damage, double multipler, T target, int speed, Vector2 position)
    {
        GD.PushWarning("AOEProjectile requires an AreaEffect.");
    }

    protected override void Land()
    {
        BasicMeleeBuff hit = new BasicMeleeBuff(buffScene);
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

}