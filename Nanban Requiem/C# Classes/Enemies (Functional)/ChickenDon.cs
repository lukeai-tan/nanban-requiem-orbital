using System;
using Godot;

public partial class ChickenDon : BasicMeleeEnemy
{
    [Export]
    public float speedGrowthMultiplier = 2.0f;
    private int originalSpeed;
    [Export]
    public int maxSpeed = 500;
    bool phaseTwoStarted = false;
    bool phaseThreeStarted = false;
    int phaseOneHealth;
    private float lifetime = 0f;
    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        base._Ready();
        phaseOneHealth = this.health;
        originalSpeed = this.movementSpeed;
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
            if (phaseTwoStarted)
            {
                lifetime += (float)delta;
                movementSpeed = Mathf.Clamp(
                    Mathf.RoundToInt(originalSpeed * Mathf.Pow(speedGrowthMultiplier, lifetime)),
                    originalSpeed,
                    maxSpeed
                );
            }
        }
        timeSinceLastAttack += delta;
    }

    private void HandleAnimation()
    {
        if (animation == null)
            return;

        if (health <= phaseOneHealth / 2 && !phaseTwoStarted)
        {
            GD.Print("Phase 2 started");
            phaseTwoStarted = true;
            animation.Play("phase2");
        }
        else if (!phaseTwoStarted)
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