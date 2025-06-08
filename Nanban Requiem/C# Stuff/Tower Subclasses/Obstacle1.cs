using System;
using Godot;

public partial class Obstacle1 : ObstacleBase
{
    public new void Initialize()
    {
        this.health = 1500;
        this.physDefense = 50;
        this.artsDefense = 50;
        this.blockCount = 2;
        this.range = (TowerBlockRange)((PackedScene)GD.Load("res://path/to/TowerBlockRange.tscn")).Instantiate();
        base.Initialize();
    }

    public override string ToString()
    {
        return "Obstacle 1: " + base.ToString();
    }

}