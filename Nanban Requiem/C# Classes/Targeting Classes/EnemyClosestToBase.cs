using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class EnemyClosestToBase : ITargeting<Enemy>
{

    public Enemy GetTarget(List<Enemy> targets)
    {
        List<Enemy> targetable = targets.Where(enemy => enemy.CanTarget() && enemy.GetProgress() >= 0f).ToList();
        targetable.Sort((e1, e2) => e1.GetProgress().CompareTo(e2.GetProgress()));
        return targetable.LastOrDefault();
    }

    public List<Enemy> GetTargets(List<Enemy> targets)
    {
        return targets;
    }

}