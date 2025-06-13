using System;
using Godot;

public partial class BasicRangedBuff : BasicRangedAttack
{

    protected PackedScene buffScene;

    public BasicRangedBuff(PackedScene projectileScene, Node2D initiator, PackedScene buffScene) : base(projectileScene, initiator)
    {
        this.buffScene = buffScene;
    }

    public override void Execute<T>(T target)
    {
        if (this.IsUsable())
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map1/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is ProjectileB<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition, buffScene);
                projectilesNode.AddChild(projectile);
            }
        }
    }

}