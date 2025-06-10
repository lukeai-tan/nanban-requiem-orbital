using System;
using System.Collections.Generic;
using Godot;

// For melee towers, acts as a hitbox for blocking enemies
public partial class TowerBlockRange : DetectionRange<Enemy>
{

    protected ObstacleBase owner;
    protected int totalBlock;
    protected int currentBlock;

    public override void _Ready()
    {
        this.owner = this.GetParentOrNull<ObstacleBase>();
        base._Ready();
    }

    public void Initialize()
    {
        this.totalBlock = this.owner.GetBlockCount();
        this.currentBlock = this.totalBlock;
    }

    protected override void OnUnitEntered(Enemy enemy)
    {
        if (enemy is IBlock blockable && !blockable.IsBlocked())
        {
            int cost = blockable.GetBlockCount();
            if (cost <= this.currentBlock)
            {
                blockable.Blocked(this.owner);
                this.currentBlock -= cost;
                base.OnUnitEntered(enemy);
            }
        }
    }

    protected override void OnUnitExited(Enemy enemy)
    {
        if (this.targetsInRange.Contains(enemy))
        {
            IBlock blocker = (IBlock)enemy;
            this.currentBlock += blocker.GetBlockCount();
            base.OnUnitExited(enemy);
        }
    }

    public override string ToString()
    {
        return this.owner.ToString() + " Blocking: " + base.ToString();
    }

}
