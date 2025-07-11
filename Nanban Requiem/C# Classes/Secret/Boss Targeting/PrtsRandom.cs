using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class PrtsRandom : ITargeting<Tower>
{

    protected Random rng = new Random();

    public Tower GetTarget(List<Tower> targets)
    {
        int idx = this.rng.Next(targets.Count);
        return targets[idx];
    }

    public List<Tower> GetTargets(List<Tower> targets)
    {
        return targets;
    }

}