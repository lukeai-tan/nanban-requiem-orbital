using System;
using Godot;

public class BasicRangedAttack : Action
{

    protected Attack attack;
    protected int damage = 0;
    protected double multiplier = 1;
    protected Node2D initiator;
    protected int projectileSpeed;
    protected PackedScene projectileScene;

    public BasicRangedAttack(PackedScene projectileScene, Node2D initiator)
    {
        this.priority = 1;
        this.projectileScene = projectileScene;
        this.initiator = initiator;
    }

    public void SetAttackAndSpeed(Attack attack, int projectileSpeed)
    {
        this.attack = attack;
        this.projectileSpeed = projectileSpeed;
        this.usable = true;
    }

    public void SetModifiers(int damage, double multiplier)
    {
        this.damage = damage;
        this.multiplier = multiplier;
    }

    public override void Execute<T>(T target)
    {
        if (this.isUsable())
        {
            Node2D projectilesNode = this.initiator.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map1/Projectiles");
            var projectileInstance = this.projectileScene.Instantiate();
            if (projectileInstance is ProjectileA<T> projectile)
            {
                projectile.Initialize(this.attack, this.damage, this.multiplier, target, this.projectileSpeed, this.initiator.GlobalPosition);
                projectilesNode.AddChild(projectile);
            }
        }
    }

    public override string ToString()
    {
        return "Basic Ranged: " + this.attack.ToString() + " from " + this.initiator.ToString() + base.ToString();
    }

}