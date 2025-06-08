using System;
using Godot;
public partial class MightyWhitey : BasicMeleeEnemy
{
    public new void Initialize()
    {
        this.health = 150;
        this.physDefense = 0;
        this.artsDefense = 0;
        this.movementSpeed = 200;
        this.meleeAttack = new ArtsAttack();
        this.meleeDamage = 50;
        this.attackSpeed = 2;
        this.blockCount = 1;
        base.Initialize();
    }

    public override string ToString()
    {
        return "White: " + this.meleeDamage.ToString() + " " + base.ToString();
    }

}