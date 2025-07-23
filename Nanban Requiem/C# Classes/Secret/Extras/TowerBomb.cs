using System;
using Godot;

public partial class TowerBomb : Buff
{

    public IAct marked;
    public event EventHandler Next;
    public event EventHandler Return;
    protected bool spread = true;

    public override void Activate(IBuffable target)
    {
        if (target is Tower && target is IAct marked)
        {
            this.marked = marked;
            this.marked.TakeAction += this.CheckHit;
        }
        this.activated = true;
    }

    public override void _Process(double delta)
    {
        this.animation.Play("default");
        base._Process(delta);
    }

    protected void CheckHit(object enemy, EventArgs e)
    {
        if (enemy is Prts)
        {
            this.spread = false;
            this.IsExpired();
            this.Return?.Invoke(marked, EventArgs.Empty);
        }
    }

    public override void Deactivate()
    {
        if (spread)
        {
            this.Next?.Invoke(marked, EventArgs.Empty);
        }
        base.Deactivate();
    }

    public override void _ExitTree()
    {
        if (this.marked != null)
        {
            this.marked.TakeAction -= this.CheckHit;
        }
    }

}