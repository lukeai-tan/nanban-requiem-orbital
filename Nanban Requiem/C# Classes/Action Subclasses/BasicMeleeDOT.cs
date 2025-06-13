using System;
using Godot;

public class BasicMeleeDOT : BasicMeleeAttack
{

    protected PackedScene dotScene;

    public BasicMeleeDOT(PackedScene dotScene) : base()
    {
        this.dotScene = dotScene;
    }

    public override void Execute<T>(T target)
    {
        base.Execute<T>(target);
        if (this.IsUsable())
        {
            Node effect = this.dotScene.Instantiate();
            if (effect is DamageOverTime dot)
            {
                target.AddChild(dot);
                dot.Initialize(target, this.damage);
            }
        }
    }

}