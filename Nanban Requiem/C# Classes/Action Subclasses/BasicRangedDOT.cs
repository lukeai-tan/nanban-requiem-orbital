using System;
using Godot;

public partial class BasicRangedDOT : BasicRangedAttack
{

    protected PackedScene dotScene;

    public BasicRangedDOT(PackedScene projectileScene, Node2D initiator, PackedScene dotScene) : base(projectileScene, initiator)
    {
        this.dotScene = dotScene;
    }

    public override void Execute<T>(T target)
    {
        if (this.isUsable())
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map1/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is ProjectileD<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition, dotScene);
                projectilesNode.AddChild(projectile);
            }
        }
    }

}