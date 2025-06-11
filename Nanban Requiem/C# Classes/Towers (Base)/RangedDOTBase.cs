using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and area damage ranged attacks
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene,
// attackSpeed, range, targeting, buffScene
public abstract partial class RangedDOTBase : RangedTowerBase
{

    [Export] protected PackedScene dotScene;

    public override void SetActions()
    {
        this.basicRanged = new BasicRangedDOT(this.projectileScene, this, this.dotScene);
        this.basicRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
    }

    public override string ToString()
    {
        return "DOT " + base.ToString();
    }

}