using System;
using System.Collections.Generic;
using Godot;

// For ranged enemies
public partial class EnemyDetectionRange : DetectionRange<Tower>
{

    protected Enemy owner;

    public override void _Ready()
    {
        this.owner = this.GetParentOrNull<Enemy>();
        base._Ready();
    }

    public override string ToString()
    {
        return this.owner.ToString() + " Detecting: " + base.ToString();
    }

}