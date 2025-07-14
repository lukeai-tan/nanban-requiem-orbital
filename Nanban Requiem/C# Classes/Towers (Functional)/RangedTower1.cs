using System;
using Godot;

public partial class RangedTower1 : RangedTowerBase
{
    public override void _Ready()
    {
        this.rangedAttack = new PhysicalAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
    }

    public override string ToString()
    {
        return "Ranged Tower 1: " + this.attack.ToString() + " " + base.ToString();
    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                GD.Print($"Tower clicked!");
            }
        }
    }

}