using System;
using Godot;

public partial class AOERangedAttack : BasicRangedAttack
{

    protected IAreaEffect areaEffect;

    public AOERangedAttack(PackedScene projectileScene, Node2D initiator, IAreaEffect areaEffect) : base(projectileScene, initiator)
    {
        this.areaEffect = areaEffect;
    }

    public override void Execute<T>(T target)
    {
        if (this.isUsable() && this.areaEffect is AreaEffect<T> effect)
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map1/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is AOEProjectile<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition, effect);
                projectilesNode.AddChild(projectile);
            }
        }
    }

}