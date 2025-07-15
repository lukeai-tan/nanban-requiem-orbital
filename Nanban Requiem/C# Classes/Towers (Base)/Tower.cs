using System;
using System.Collections.Generic;
using Godot;

// Layer 0 tower logic that handles hit response
// Required fields: health, physDefense, artsDefense
public abstract partial class Tower : Unit
{

    public virtual void Signal(Enemy target)
    {

    }

    public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
            {
                var towerUI = GetNode<TowerUI>("/root/SceneHandler/GameScene/UI/HUD/TowerUI");
                towerUI.ShowTowerUI(this);
                GD.Print("Tower clicked!");
            }
        }
    }

    public void Retreat()
    {
        GD.Print("Tower retreated!");
        QueueFree();
    }

    
}