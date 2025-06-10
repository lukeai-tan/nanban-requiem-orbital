using System;
using Godot;

public partial class Samurai : BasicMeleeEnemy
{

    public override void _Ready()
    {
        this.health = 500;
        this.physDefense = 5;
        this.artsDefense = 5;
        this.movementSpeed = 100;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 100;
        this.attackSpeed = 1;
        this.blockCount = 1;
        base._Ready();
    }

    public override string ToString()
    {
        return "Samurai: " + this.meleeDamage.ToString() + " " + base.ToString();
    }
    
}