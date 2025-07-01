using System;
using System.Collections.Generic;
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

    public T GetTarget(List<T> targets)
    {
        return null;
    }

    public virtual List<T> GetTargets(List<T> targets)
    {
        int num = this.numTargets;
        int count = targets.Count;
        if (count > num)
        {
            targets.RemoveRange(num, count - num);
        }
        return targets;
    }

}