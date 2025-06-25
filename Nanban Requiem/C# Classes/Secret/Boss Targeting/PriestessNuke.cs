using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// Always nukes furthest towers
public partial class PriestessNuke : BossTargeting<Tower>
{

    protected Priestess self;

    public PriestessNuke(Priestess self)
    {
        this.self = self;
    }

    public override List<Tower> GetTargets(List<Tower> targets)
    {
        List<Tower> targetable = targets.Where(target => target.CanTarget()).ToList();
        Vector2 position = this.self.GlobalPosition;
        targetable.Sort((t1, t2) => -position.DistanceTo(t1.GlobalPosition).CompareTo(position.DistanceTo(t2.GlobalPosition)));
        return base.GetTargets(targetable);
    }

}