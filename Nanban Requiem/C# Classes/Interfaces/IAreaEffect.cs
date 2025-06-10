using System;
using Godot;

// An interface for area effects
public interface IAreaEffect
{
    public void Activate(Vector2 position, Action effect);

    public void Effect();

}