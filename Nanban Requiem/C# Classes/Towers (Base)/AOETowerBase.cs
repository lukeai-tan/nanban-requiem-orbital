using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and area damage ranged attacks
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene,
// attackSpeed, range, targeting, areaEffectScene
public abstract partial class AOETowerBase : RangedTowerBase
{

    [Export] protected PackedScene areaEffectScene;

    public override void SetActions()
    {
        this.basicRanged = new AOERangedAttack(this.projectileScene, this, this.areaEffectScene);
        this.basicRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
    }

    public override string ToString()
    {
        return "AOE " + base.ToString();
    }


}