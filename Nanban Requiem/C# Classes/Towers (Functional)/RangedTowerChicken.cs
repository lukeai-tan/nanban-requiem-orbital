using System;
using Godot;

public partial class RangedTowerChicken : RangedTowerBase{
    public override void _Ready()
    {
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }
}
