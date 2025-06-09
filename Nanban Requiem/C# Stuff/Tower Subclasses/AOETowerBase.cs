using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and area damage ranged attacks
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene,
// attackSpeed, range, targeting, areaEffect
public abstract partial class AOETowerBase : RangedTowerBase
{

    [Export] protected PackedScene areaEffectScene;
    protected AOERangedAttack aoeRanged;

    public override void _Ready()
    {
        this.aoeRanged = new AOERangedAttack(this.projectileScene, this, this.areaEffectScene);
        this.aoeRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
        base._Ready();
    }

    public override void Act()
    {
        if (this.timeSinceLastAttack >= 1f / this.attackSpeed)
        {
            Enemy target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                this.aoeRanged.SetModifiers(this.rangedDamage, 1);
                this.aoeRanged.Execute(target);
            }
        }
    }
    public override string ToString()
    {
        return "AOE " + base.ToString();
    }


}