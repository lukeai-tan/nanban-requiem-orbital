using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime;
using Godot;

public abstract partial class BossTargeting<T> : ITargeting<T>
    where T : Unit
{

    protected int numTargets = 0;

    public void SetTargets(int numTargets)
    {
        this.numTargets = numTargets;
    }

    public virtual T GetTarget(List<T> targets)
    {
        return null;
    }

    public virtual List<T> GetTargets(List<T> targets)
    {
        int count = targets.Count;
        if (count == 0 || this.numTargets == 0)
        {
            return [];
        }
        List<T> chosen = [];
        for (int i = 0; i < Math.Min(this.numTargets, count); i++)
        {
            chosen.Add(targets[i]);
        }
        return chosen;
    }

}