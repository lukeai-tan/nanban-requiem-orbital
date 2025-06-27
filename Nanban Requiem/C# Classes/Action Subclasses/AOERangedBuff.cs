using System;
using Godot;

public class AOERangedBuff : AOERangedAttack
{

    protected PackedScene buffScene;

    public AOERangedBuff(PackedScene projectileScene, Node2D initiator, PackedScene areaEffectScene, PackedScene buffScene) : base(projectileScene, initiator, areaEffectScene)
    {
        this.buffScene = buffScene;
    }

    public override void Execute<T>(T target)
    {
        if (this.IsUsable())
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is ProjectileAB<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition, areaEffectScene, this.buffScene);
                projectilesNode.AddChild(projectile);
            }
        }
    }


}