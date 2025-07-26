using System;
using System.ComponentModel;
using Godot;

public partial class ChickenDon : BasicRangedEnemy
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
        this.rangedAttack = new ArtsAttack();
        this.targeting = new TowerClosestToSelf(this);
        base._Ready();
        phaseOneHealth = this.health;
        originalSpeed = this.movementSpeed;
        // CallSkillName("Chicken Don", "white");
    }

    public override void _Process(double delta)
    {
        if (!initialized)
            return;

        if (phaseThreeStarted && !IsBlocked())
        {
            Act();
            base.Move(delta);
        }
        else
        {
            HandleAnimation();
            base.Move(delta);
            if (phaseTwoStarted && !phaseThreeStarted)
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
            ChangeBackground();
            CallSkillName("Eyes Over Heaven", "black");
        }

        if (health <= phaseOneHealth / 4 && !phaseThreeStarted)
        {
            CallSkillName("Complete Global Oblivion", "black");
            GD.Print("Phase 3 started");
            phaseThreeStarted = true;
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

    public async void ChangeBackground()
    {
        var backgroundOne = GetNode<TextureRect>("/root/SceneHandler/GameScene/Map/Background");
        var backgroundTwo = GetNode<TextureRect>("/root/SceneHandler/GameScene/Map/Background2");
        backgroundTwo.Visible = true;
        backgroundTwo.Modulate = new Color(1, 1, 1, 0);

        var tween = GetTree().CreateTween();

        tween.TweenProperty(backgroundOne, "modulate:a", 0.0f, 1.0f);
        tween.TweenProperty(backgroundTwo, "modulate:a", 1.0f, 1.0f);
        await ToSignal(tween, "finished");

        backgroundOne.Visible = false;
    }

    public void CallSkillName(String text, String colour)
    {
        var titleCard = GetNode("/root/SceneHandler/GameScene/UI/TitleCard");
        if (colour == "black")
        {
            titleCard.Call("black_title_card");
        }
        else
        {
            titleCard.Call("white_title_card");
        }
        titleCard.Call("activate_title_sequence", text);
        GD.Print("Showed Title Card");
    }
}