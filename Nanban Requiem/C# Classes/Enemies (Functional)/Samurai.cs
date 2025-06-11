using System;
using Godot;

public partial class Samurai : BasicMeleeEnemy
{

    public override void _Ready()
    {
        this.meleeAttack = new PhysicalAttack();
        this.Scale = new Vector2(2f, 2f);
        this.Position = new Vector2(0, -25);
        base._Ready();
    }

    public override string ToString()
    {
        return "Samurai: " + this.meleeDamage.ToString() + " " + base.ToString();
    }
    
}