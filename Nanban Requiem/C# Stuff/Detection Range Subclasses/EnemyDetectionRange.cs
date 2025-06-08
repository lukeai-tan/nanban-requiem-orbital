using System;
using System.Collections.Generic;
using Godot;

// For ranged enemies
public partial class EnemyDetectionRange : DetectionRange<Tower>
{

    protected Enemy owner;

    public void Initialize(Enemy owner)
    {
        this.targetsInRange = new List<Tower>();
        this.owner = owner;
    }

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}