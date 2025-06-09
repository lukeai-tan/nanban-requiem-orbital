using System;
using Godot;

public partial class Obstacle1 : ObstacleBase
{
    public override void _Ready()
    {
        this.health = 1500;
        this.physDefense = 50;
        this.artsDefense = 50;
        this.blockCount = 2;
        base._Ready();
    }

    public override string ToString()
    {
        return "Obstacle 1: " + base.ToString();
    }

}