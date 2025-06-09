using System;
using System.Buffers;
using System.Collections.Generic;
using Godot;

// For ranged towers
public partial class TowerDetectionRange : DetectionRange<Enemy>
{

    protected Tower owner;

    public override void _Ready()
    {
        this.owner = this.GetParentOrNull<Tower>();
        base._Ready();
    }

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}