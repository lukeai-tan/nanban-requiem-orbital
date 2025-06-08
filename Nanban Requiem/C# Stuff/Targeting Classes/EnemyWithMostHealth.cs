using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class EnemyWithMostHealth : ITargeting<Enemy>
{

    public Enemy GetTarget(List<Enemy> targets)
    {
        List<Enemy> targetable = targets.Where(enemy => enemy.CanTarget() && enemy.GetProgress() >= 0f).ToList();
        targetable.Sort((e1, e2) => e1.GetHealth().CompareTo(e2.GetHealth()));
        return targetable.FirstOrDefault();
    }

}