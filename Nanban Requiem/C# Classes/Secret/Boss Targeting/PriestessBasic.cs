using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// Attacks towers with highest effective attack
public partial class PriestessBasic : BossTargeting<Tower>
{

    protected Priestess self;

    public PriestessBasic(Priestess self)
    {
        this.self = self;
    }

    public override List<Tower> GetTargets(List<Tower> targets)
    {
        List<Tower> targetable = targets.Where(target => target.CanTarget()).ToList();
        Vector2 position = this.self.GlobalPosition;
        targetable.Sort((t1, t2) => -t1.GetAttack().CompareTo(t2.GetAttack()));
        return base.GetTargets(targetable);
    }

}