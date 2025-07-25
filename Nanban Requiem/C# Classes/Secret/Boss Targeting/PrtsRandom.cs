using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class PrtsRandom : BossTargeting<Tower>
{

    protected Random rng = new Random();

    public override Tower GetTarget(List<Tower> targets)
    {
        if (targets.Count == 0)
        {
            return null;
        }
        return targets[rng.Next(targets.Count)];
    }

    public override List<Tower> GetTargets(List<Tower> targets)
    {
        targets = targets.OrderBy(_ => rng.Next()).ToList();
        return base.GetTargets(targets);
    }

}