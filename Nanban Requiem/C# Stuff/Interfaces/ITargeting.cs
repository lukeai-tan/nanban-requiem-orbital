using System;
using System.Collections.Generic;
using Godot;

// Interface for all target picking classes
public interface ITargeting<T>
    where T : Node2D, IUnit
{

    public abstract T GetTarget(List<T> targets);

}