using System;
using Godot;

public partial class RangedTowerChicken : RangedTowerBase
{
    private float originalHealth;
    private float originalAttack;
    public override void _Ready()
    {
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new EnemyClosestToBase();
        this.originalHealth = this.health;
        this.originalAttack = this.attack;
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        CompleteGlobalDominion();
    }

    public void CompleteGlobalDominion()
    {
        float healthPercentage = Mathf.Clamp(this.health / this.originalHealth, 0.01f, 1f);
        float multiplier = 1f / healthPercentage;
        float maxMultiplier = 3f;
        multiplier = Mathf.Min(multiplier, maxMultiplier);
        this.attack = (int)(originalAttack * multiplier);
    }
}
