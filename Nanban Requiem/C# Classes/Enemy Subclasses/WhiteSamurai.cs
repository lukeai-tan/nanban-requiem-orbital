using System;
using Godot;
public partial class WhiteSamurai : BasicMeleeEnemy
{
    public override void _Ready()
    {
        this.health = 200;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 200;
        this.meleeAttack = new ArtsAttack();
        this.meleeDamage = 50;
        this.attackSpeed = 2;
        this.blockCount = 1;
        this.Scale = new Vector2(2f, 2f);
        this.Position = new Vector2(0, -25);
        base._Ready();
    }
    
    public override string ToString()
    {
        return "White: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}