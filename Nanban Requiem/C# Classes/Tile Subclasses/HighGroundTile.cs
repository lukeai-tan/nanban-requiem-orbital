using System;
using Godot;

public partial class LowGroundTile : Tile
{
    public void Occupy(RangedTowerBase occupant)
    {
        this.occupant = occupant;
        this.occupant.Despawning += (object occupant, EventArgs e) => this.Clear();
        this.occupied = true;
    }

}