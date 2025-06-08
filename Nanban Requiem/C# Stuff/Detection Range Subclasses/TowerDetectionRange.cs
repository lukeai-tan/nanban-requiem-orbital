using System;
using System.Buffers;
using System.Collections.Generic;
using Godot;

// For ranged towers
public partial class TowerDetectionRange : DetectionRange<Enemy>
{

    protected Tower owner;

    public void Initialize(Tower owner)
    {
        this.targetsInRange = new List<Enemy>();
        this.owner = owner;
    }

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}