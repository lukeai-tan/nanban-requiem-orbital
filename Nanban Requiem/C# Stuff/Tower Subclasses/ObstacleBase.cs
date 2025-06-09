using System;
using Godot;

// Layer 1 tower logic that handles blocking
// Required fields: health, physDefense, artsDefense, blockCount, range
public abstract partial class ObstacleBase : Tower
{

    protected int blockCount;
    [Export] protected TowerBlockRange range;

    public int GetBlockCount()
    {
        return this.blockCount;
    }

    public override void _ExitTree()
    {
        this.range.QueueFree();
        base._ExitTree();
    }

}