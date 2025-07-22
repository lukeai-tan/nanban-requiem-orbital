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
        if (!initialized)
            return;

        if (IsBlocked())
        {
            Act();
        }
        else
        {
            HandleAnimation();
            base.Move(delta);
        }

        timeSinceLastAttack += delta;
    }

    private void HandleAnimation()
    {
        if (animation == null)
            return;

        if (phaseTwoStarted)
        {
            animation.Play("phase2");
        }
        else if (health <= phaseOneHealth / 2)
        {
            GD.Print("Phase 2 started for ChickenDon");
            movementSpeed *= 5;
            phaseTwoStarted = true;
        }
        else
        {
            animation.Play("running");
        }
    }

    public override void ReachedObjective(object pathing, EventArgs e)
    {
        this.EmitSignal(nameof(DamageBase), 1000);
        this.Despawn();
    }
}