using System;
using System.Threading;
using Godot;

// Implementation of pathing using PathFollow2D
public class BasicEnemyPathing : IPathing
{

    public event EventHandler PathCompletion;
    protected int speed;
    protected Enemy self;
    protected Path2D path;
    protected PathFollow2D pathing;
    protected float completion;

    public BasicEnemyPathing(int speed, Enemy self)
    {
        this.speed = speed;
        this.self = self;
        this.self.Despawning += this.ClearPath;
    }

    public void InitializePath(Path2D path)
    {
        this.path = path;
        if (this.pathing == null)
        {
            this.pathing = new PathFollow2D();
        }
        this.path.AddChild(this.pathing);
        this.pathing.AddChild(this.self);
        this.pathing.Progress = 0f;
        this.completion = this.path.Curve.GetBakedLength();
    }

    public void Update(double delta)
    {
        if (pathing == null)
        {
            return;
        }
        else if (this.completion - this.pathing.Progress <= 5f)
        {
            this.PathCompletion?.Invoke(this, EventArgs.Empty);
            this.pathing.QueueFree();
            this.pathing = null;
        }
        else
        {
            this.pathing.Progress += this.speed * (float) delta;
        }
    }

    public float GetProgress()
    {
        if (pathing != null)
        {
            return pathing.Progress;
        }
        else
        {
            return -1f;
        }
    }

    public void ClearPath(object target, EventArgs e) 
    {
        this.pathing.QueueFree();
    }

    public override string ToString()
    {
        return this.self.ToString() + " On Path: " + this.path.ToString();
    }

}