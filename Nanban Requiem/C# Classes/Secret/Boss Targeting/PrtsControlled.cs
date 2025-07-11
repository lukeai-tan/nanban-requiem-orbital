using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class PrtsControlled : ITargeting<Tower>
{

    protected Tower owner;

    public PrtsControlled(Tower owner)
    {
        this.owner = owner;
    }

    public Tower GetTarget(List<Tower> targets)
    {
        List<Tower> targetable = targets.Where(tower => !tower.Equals(this.owner) && tower.CanTarget()).ToList();
        Vector2 position = this.owner.GlobalPosition;
        targetable.Sort((e1, e2) => position.DistanceTo(e1.GlobalPosition).CompareTo(position.DistanceTo(e2.GlobalPosition)));
        return targetable.FirstOrDefault();
    }

    public List<Tower> GetTargets(List<Tower> targets)
    {
        return targets;
    }

}