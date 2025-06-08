using System;
using Godot;

public partial class Samurai : BasicMeleeEnemy
{
    public new void Initialize()
    {
        this.health = 300;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 100;
        this.meleeAttack = new PhysicalAttack();
        this.meleeDamage = 100;
        this.attackSpeed = 0.5;
        this.blockCount = 1;
        base.Initialize();
    }
    public override string ToString()
    {
        return "Samurai: " + this.meleeDamage.ToString() + " " + base.ToString();
    }
    
}