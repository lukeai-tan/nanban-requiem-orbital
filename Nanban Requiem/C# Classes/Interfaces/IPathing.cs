using System;
using Godot;

// An interface to define an enemy's pathing logic
public interface IPathing
{

    public event EventHandler PathCompletion;

    public void Update(float progress);

    public float GetProgress();

}