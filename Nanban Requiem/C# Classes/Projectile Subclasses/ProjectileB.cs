using System;
using Godot;

public partial class ProjectileB<T> : ProjectileA<T>
    where T : Node2D, IUnit
{
    protected PackedScene buffScene;

    public void Initialize(Attack attack, int damage, double multiplier, T target, int speed, Vector2 position, PackedScene buffScene)
    {
        this.buffScene = buffScene;
        base.Initialize(attack, damage, multiplier, target, speed, position);
    }

    public override void Initialize(Attack attack, int damage, double multipler, T target, int speed, Vector2 position)
    {
        GD.PushWarning("BuffProjectile requires a Buff.");
    }

    protected override void Land()
    {
        Node buff = this.buffScene.Instantiate();
        if (buff is Buff effect && this.target is IBuffable buffable)
        {
            buffable.ReceiveBuff(effect);
        }
        base.Land();
    }

    public override string ToString()
    {
        return "Buff " + base.ToString();
    }

}