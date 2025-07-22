using System;
using Godot;

public partial class ChickenDon : BasicMeleeEnemy
{

    bool phaseTwoStarted = false;
    int phaseOneHealth;
    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        base._Ready();
        phaseOneHealth = this.health;
    }

    public override void _Process(double delta)
    {
        if (this.initialized)
        {
            if (this.IsBlocked())
            {
                this.Act();
            }
            else
            {
                if (this.animation != null)
                {
                    if (phaseTwoStarted)
                    {
                        this.animation.Play("phase2");
                    }
                    else if (!phaseTwoStarted && this.health <= phaseOneHealth / 2)
                    {
                        GD.Print("Phase 2 started for ChickenDon");
                        this.movementSpeed *= 5;
                        phaseTwoStarted = true;
                    }
                    else
                    {
                        this.animation.Play("running");
                    }
                }
                base.Move(delta);
            }
            this.timeSinceLastAttack += delta;
        }
    }

}