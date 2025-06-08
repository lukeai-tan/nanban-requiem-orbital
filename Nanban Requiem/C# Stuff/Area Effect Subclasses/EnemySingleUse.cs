using System;
using Godot;

// Any single use area effect from enemies
public partial class EnemySingleUse : AreaEffect<Tower>
{

    public override void _Ready()
    {
        if (this.active)
        {
            base.Effect();
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "Single Instance Enemy AOE: " + base.ToString();
    }

}