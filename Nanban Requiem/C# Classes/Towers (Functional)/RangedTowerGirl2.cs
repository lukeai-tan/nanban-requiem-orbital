using System;
using System.Collections.Generic;
using Godot;

public partial class RangedTowerGirl2 : RangedBufferBase
{

    public override void _Ready()
    {
        this.rangedAttack = new ArtsAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower Girl 2: " + this.attack.ToString() + " " + base.ToString();
    }

}