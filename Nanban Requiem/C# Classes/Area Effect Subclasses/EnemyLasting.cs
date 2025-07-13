using System;
using Godot;

// Any single use area effect from enemies
public partial class EnemyLasting : AreaEffect<Tower>
{

    [Export] protected double duration;
    [Export] protected double cooldown;
    protected double timeToNextSkill = 1;

    public override void _PhysicsProcess(double delta)
    {
        if (this.active)
        {
            if (this.duration < 0)
            {
                this.QueueFree();
            }
            else if (this.timeToNextSkill <= 0)
            {
                base.Effect();
                this.timeToNextSkill = this.cooldown;
            }
            this.duration -= delta;
            this.timeToNextSkill -= delta;
        }
    }

    public override string ToString()
    {
        return "Single Instance Enemy AOE: " + base.ToString();
    }

}