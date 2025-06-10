using System;
using Godot;

// Any single use area effect from enemies
public partial class EnemySingleUse : AreaEffect<Tower>
{

    public async override void _Process(double delta)
    {
        if (this.active)
        {
            await ToSignal(GetTree(), "physics_frame");
            base.Effect();
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "Single Instance Enemy AOE: " + base.ToString();
    }

}