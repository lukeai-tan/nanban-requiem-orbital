using System;
using Godot;
public partial class MightyWhitey : BasicMeleeEnemy
{
    public override void _Ready()
    {
        this.health = 150;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 200;
        this.meleeAttack = new ArtsAttack();
        this.meleeDamage = 50;
        this.attackSpeed = 2;
        this.blockCount = 1;
    }
    
    public override string ToString()
    {
        return "White: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}