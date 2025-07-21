using System;
using Godot;

// Any single use area effect from enemies
public partial class GeneralSingleUse : AreaEffect<Unit>
{

    public async override void _PhysicsProcess(double delta)
    {
        if (this.active)
        {
            this.active = false;
            await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
            base.Effect();
            this.QueueFree();
        }
    }

    public override string ToString()
    {
        return "Single Instance AOE: " + base.ToString();
    }

}