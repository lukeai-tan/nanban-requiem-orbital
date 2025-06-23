using System;
using Godot;

public abstract partial class Tile : Area2D
{

    protected int x;
    protected int y;
    protected bool occupied = false;
    protected Tower occupant = null;

    public Vector2 GetCoords()
    {
        return new Vector2(this.x, this.y);
    }

    public void SetCoords(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    protected void Clear()
    {
        this.occupant = null;
        this.occupied = false;
    }

}