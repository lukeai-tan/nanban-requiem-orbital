using System;
using Godot;

public abstract partial class ProjectileD<T> : ProjectileA<T>
    where T : Unit
{
    protected PackedScene dotScene;

    public void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position, PackedScene dotScene)
    {
        this.dotScene = dotScene;
        base.Initialize(attack, damage, multiplier, target, speed, position);
    }

    public override void Initialize(Attack attack, int damage, double multipler, T target, int speed, Vector2 position)
    {
        GD.PushWarning("DOTProjectile requires a DOT.");
    }

    protected override void Land()
    {
        Node effect = this.dotScene.Instantiate();
        if (effect is DamageOverTime dot)
        {
            this.target.AddChild(dot);
            dot.Initialize(this.target, this.damage);
        }
        base.Land();
    }

    public override string ToString()
    {
        return "Buff " + base.ToString();
    }

}