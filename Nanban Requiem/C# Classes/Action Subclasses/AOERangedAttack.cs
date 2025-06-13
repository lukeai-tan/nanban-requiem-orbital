using System;
using Godot;

public partial class AOERangedAttack : BasicRangedAttack
{

    protected PackedScene areaEffectScene;

    public AOERangedAttack(PackedScene projectileScene, Node2D initiator, PackedScene areaEffectScene) : base(projectileScene, initiator)
    {
        this.areaEffectScene = areaEffectScene;
    }

    public override void Execute<T>(T target)
    {
        if (this.IsUsable())
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map1/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is ProjectileAA<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition, areaEffectScene);
                projectilesNode.AddChild(projectile);
            }
        }
    }

}