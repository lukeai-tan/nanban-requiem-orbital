using System;
using Godot;

public partial class Obstacle1 : ObstacleBase
{
    public override void _Ready()
    {
        this.health = 1200;
        this.physDefense = 30;
        this.artsDefense = 30;
        this.blockCount = 2;
        base._Ready();
    }

    public override string ToString()
    {
        return "Obstacle 1: " + base.ToString();
    }

}