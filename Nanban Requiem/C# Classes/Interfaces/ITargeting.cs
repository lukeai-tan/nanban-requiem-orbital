using System;
using System.Collections.Generic;
using Godot;

// Interface for all target picking classes
public interface ITargeting<T>
    where T : Unit
{

    public abstract T GetTarget(List<T> targets);
    
    public abstract List<T> GetTargets(List<T> targets);

}