using System;
using Godot;

public partial class TileManager : Node2D
{

    [Signal]
    public delegate void MapLoadedEventHandler();
    protected Vector2 tileSize = new Vector2(32, 32);
    // original screen size is 40 x 22.5
    protected Vector2 girdSize = new Vector2(42, 24);

}