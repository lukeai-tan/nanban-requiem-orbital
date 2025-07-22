using System;
using System.Runtime.CompilerServices;
using Godot;

public partial class AOEMeleeAttack : Action
{
    protected BasicMeleeAttack hit;
    protected PackedScene areaEffectScene;

    public AOEMeleeAttack(PackedScene areaEffectScene)
    {
        this.priority = 1;
        this.areaEffectScene = areaEffectScene;
        this.hit = new BasicMeleeAttack();
    }

    public void SetAttack(Attack attack)
    {
        this.hit.SetAttack(attack);
        this.usable = true;
    }

    public void SetModifiers(int damage, double multiplier)
    {
        this.hit.SetModifiers(damage, multiplier);
    }

    public override void Execute<T>(T target)
    {
        if (this.IsUsable())
        {
            Node areaEffect = this.areaEffectScene.Instantiate();
            if (areaEffect is AreaEffect<T> effect)
            {
                Node2D projectilesNode = target.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map/Projectiles");
                projectilesNode.AddChild(effect);
                effect.Activate(target.GlobalPosition, hit);
            }
        }
    }

    public void Execute<T>(Vector2 position, Node2D projectilesNode)
        where T : Unit
    {
        if (this.IsUsable())
        {
            Node areaEffect = this.areaEffectScene.Instantiate();
            if (areaEffect is AreaEffect<T> effect)
            {
                projectilesNode.AddChild(effect);
                effect.Activate(position, hit);
            }
        }
    }

}