using System;
using System.Buffers;
using System.Collections.Generic;
using Godot;

// For ranged towers
public partial class TowerDetectionRange : DetectionRange<Enemy>
{

    [Export] protected Tower owner;

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}