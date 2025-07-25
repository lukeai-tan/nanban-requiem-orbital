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
        targets.Sort((e1, e2) => e1.GetHealth().CompareTo(e2.GetHealth()));
        return base.GetTargets(targets);
    }

}