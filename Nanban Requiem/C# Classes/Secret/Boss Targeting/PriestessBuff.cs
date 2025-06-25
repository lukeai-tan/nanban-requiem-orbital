using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// Always buffs nearest enemies
public partial class PriestessBuff : BossTargeting<Enemy>
{

    protected Priestess self;

    public PriestessBuff(Priestess self)
    {
        this.self = self;
    }

    public override List<Enemy> GetTargets(List<Enemy> targets)
    {
        Vector2 position = this.self.GlobalPosition;
        targets.Sort((e1, e2) => position.DistanceTo(e1.GlobalPosition).CompareTo(position.DistanceTo(e2.GlobalPosition)));
        return base.GetTargets(targets);
    }

}