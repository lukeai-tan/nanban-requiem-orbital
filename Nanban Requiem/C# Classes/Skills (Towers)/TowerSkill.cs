using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Godot;

public class TowerSkill
{

    protected int cost;
    protected double points;

    public TowerSkill(int cost, double initialPoints)
    {
        this.cost = cost;
        this.points = initialPoints;
    }

    public bool IsReady()
    {
        return this.points >= this.cost;
    }

    public void gainPoints(double points)
    {
        this.points += points;
    }

    protected void ResetPoints()
    {
        this.points = 0;
    }

    public void Call<T>(List<T> targets, List<Action> actions)
        where T : Unit
    {
        if (!this.IsReady() || targets.Count != actions.Count)
        {
            return;
        }
        else
        {
            for (int i = 0; i < targets.Count; i++)
            {
                T target = targets[i];
                Action action = actions[i];
                if (GodotObject.IsInstanceValid(target))
                {
                    action.Execute(target);
                }
            }
            this.ResetPoints();
        }
    }

}