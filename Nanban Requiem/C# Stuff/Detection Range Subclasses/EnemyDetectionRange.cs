using System;
using System.Collections.Generic;
using Godot;

// For ranged enemies
public partial class EnemyDetectionRange : DetectionRange<Tower>
{

    [Export] protected Enemy owner;

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}