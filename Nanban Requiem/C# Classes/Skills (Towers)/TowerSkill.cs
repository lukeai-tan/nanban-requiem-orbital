using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Godot;

public abstract partial class TowerSkill : Node2D
{

    [Export] protected int cost;
    [Export] protected double initialPoints;
    protected double points;
    protected Tower owner;

    public override void _Ready()
    {
        this.owner = this.GetParentOrNull<Tower>();
        this.points = this.initialPoints;
    }

    public override void _Process(double delta)
    {
        this.points += delta;
    }

    public bool IsReady()
    {
        return this.points >= this.cost;
    }

    protected void ResetPoints()
    {
        this.points = this.initialPoints;
    }

    public abstract void Call();

}