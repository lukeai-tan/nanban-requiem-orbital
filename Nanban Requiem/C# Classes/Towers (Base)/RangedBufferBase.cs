using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and area damage ranged attacks
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene,
// attackSpeed, range, targeting, buffScene
public abstract partial class RangedBufferBase : RangedTowerBase
{

    [Export] protected PackedScene buffScene;

    public override void SetActions()
    {
        this.basicRanged = new BasicRangedBuff(this.projectileScene, this, this.buffScene);
        this.basicRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
    }

    public override string ToString()
    {
        return "Buffer " + base.ToString();
    }

}