using System;
using Godot;

public class BasicMeleeBuff : BasicMeleeAttack
{

    protected PackedScene buffScene;

    public BasicMeleeBuff(PackedScene buffScene) : base()
    {
        this.buffScene = buffScene;
    }

    public override void Execute<T>(T target)
    {
        base.Execute<T>(target);
        if (this.IsUsable())
        {
            Node buff = this.buffScene.Instantiate();
            if (buff is Buff effect && target is IBuffable buffable)
            {
                buffable.ReceiveBuff(effect);
            }
        }
    }

}