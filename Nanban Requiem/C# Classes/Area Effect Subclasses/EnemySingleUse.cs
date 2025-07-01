using System;
using Godot;

// Any single use area effect from enemies
public partial class EnemySingleUse : AreaEffect<Tower>
{

    public async override void _PhysicsProcess(double delta)
    {
        if (this.active)
        {
            this.active = false;
            await ToSignal(GetTree(), "physics_frame");
            base.Effect();
            await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "Single Instance Enemy AOE: " + base.ToString();
    }

}