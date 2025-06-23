using System;
using System.Collections.Generic;
using System.Threading;
using Godot;

public class CustomEnemyPathing : IPathing
{
    
    public event EventHandler PathCompletion;
    protected Enemy self;
    protected List<Tile> path;
    protected Tile next = null;
    protected Tile final;

    public CustomEnemyPathing(Enemy self)
    {
        this.self = self;
    }

    public void InitializePath(List<Tile> path, Tile final)
    {
        this.path = path;
        this.final = final;
    }

    public void ChangePath(List<Tile> path, Tile final)
    {
        this.path = path;
        this.final = final;
        this.next = null;
    }

    public void Update(float progress)
    {
        if (this.self.GlobalPosition.DistanceTo(this.final.GlobalPosition) <= 5f)
        {
            this.PathCompletion?.Invoke(this, EventArgs.Empty);
            return;
        }

        if (this.next == null || this.self.GlobalPosition.DistanceTo(this.next.GlobalPosition) <= 1f)
        {
            this.GetNextTile();
        }

        if (this.next != null)
        {
            Vector2 direction = (this.next.GlobalPosition - this.self.GlobalPosition).Normalized();
            this.self.GlobalPosition += direction * progress;
        }
    }

    protected void GetNextTile()
    {
        int count = this.path.Count;
        if (count != 0)
        {
            this.next = this.path[count - 1];
            this.path.RemoveAt(count - 1);
        }
        else
        {
            this.next = null;
        }
    }

    public float GetProgress()
    {
        return this.self.GlobalPosition.DistanceTo(this.final.GlobalPosition);
    }

}