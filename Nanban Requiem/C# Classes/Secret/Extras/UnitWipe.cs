using System;
using Godot;

public class UnitWipe : Action
{

    public override void Execute<T>(T target)
    {
        if (!(target is Boss))
        {
            target.Despawn();
        }
    }

}