using Godot;
using System;

// An interface for all methods that every Action must implement
public interface IAction
{

    public void Execute<T>(T target)
        where T : Node2D, IUnit;

    public bool isUsable();

}