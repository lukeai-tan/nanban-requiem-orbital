using System;
using Godot;

// Layer 1 tower logic that handles blocking
// Required fields: health, physDefense, artsDefense, blockCount, range
public abstract partial class ObstacleBase : Tower
{

    protected int blockCount;
    protected TowerBlockRange range;

    protected override void Initialize()
    {
        this.AddChild(this.range);
        this.range.Initialize(this, this.blockCount);
        base.Initialize();
    }

    public override void _ExitTree()
    {
        this.range.QueueFree();
        base._ExitTree();
    }

}