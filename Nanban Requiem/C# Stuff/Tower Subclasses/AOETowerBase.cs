using System;
using Godot;

// Layer 2 tower logic that handles specialized detection, targeting and area damage ranged attacks
// Required fields: health, physDefense, artsDefense, rangedDamage, rangedAttack, projectileSpeed, projectileScene,
// attackSpeed, range, targeting, areaEffect
public abstract partial class AOETowerBase : RangedTowerBase
{

    protected AreaEffect<Enemy> areaEffect;
    protected AOERangedAttack aoeRanged;

    protected override void Initialize()
    {
        this.aoeRanged = new AOERangedAttack(this.projectileScene, this, this.areaEffect);
        this.aoeRanged.SetAttackAndSpeed(this.rangedAttack, this.projectileSpeed);
        base.Initialize();
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