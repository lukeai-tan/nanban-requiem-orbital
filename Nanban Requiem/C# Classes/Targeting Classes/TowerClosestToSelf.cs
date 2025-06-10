using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public class TowerClosestToSelf : ITargeting<Tower>
{

    protected Enemy self;

    public TowerClosestToSelf(Enemy self)
    {
        this.self = self;
    }

    public Tower GetTarget(List<Tower> targets)
    {
        List<Tower> targetable = targets.Where(target => target.CanTarget()).ToList();
        Vector2 position = this.self.GlobalPosition;
        targetable.Sort((e1, e2) => position.DistanceTo(e1.GlobalPosition).CompareTo(position.DistanceTo(e2.GlobalPosition)));
        return targetable.FirstOrDefault();
    } 

}