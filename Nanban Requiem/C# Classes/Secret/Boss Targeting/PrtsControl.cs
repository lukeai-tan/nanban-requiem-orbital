using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class PrtsControl : ITargeting<Tower>
{

    public Tower GetTarget(List<Tower> targets)
    {
        List<Tower> targetable = targets.Where(tower => tower.CanTarget() && tower is IAct).ToList();
        targetable.Sort((t1, t2) => t1 is RangedTowerBase ? 1 : -1);
        return targetable.LastOrDefault();
    }

    public List<Tower> GetTargets(List<Tower> targets)
    {
        return targets;
    }

}