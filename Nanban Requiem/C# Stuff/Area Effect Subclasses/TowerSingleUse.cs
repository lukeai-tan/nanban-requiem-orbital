using System;
using Godot;

// Any single use area effect from towers
public partial class TowerSingleUse : AreaEffect<Enemy>
{

    public override void _Process(double delta)
    {
        if (this.active)
        {
            base.Effect();
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "Single Instance Tower AOE: " + base.ToString();
    }

}