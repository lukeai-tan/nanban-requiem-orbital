using Godot;
using System;

public partial class TowerBuilder : Node2D
{
    public Node2D MapNode;
    public bool BuildMode = false;
    public bool BuildValid = false;
    public Vector2I BuildTile;
    public Vector2 BuildLocation;
    public string BuildType;
    public Node Ui;
    private Node towerPreviewNode = null;

    public void InitiateBuildMode(string towerType)
    {
        if (BuildMode)
        {
            CancelBuildMode();
        }

        BuildType = towerType;
        BuildMode = true;
        Ui.Call("set_tower_preview", BuildType, GetGlobalMousePosition());
    }

    public void UpdateTowerPreview()
    {
        Vector2 mousePosition = GetGlobalMousePosition();

        var towerExclusions = MapNode.GetNode<TileMapLayer>("TowerExclusions");
        var pathLayer = MapNode.GetNode<TileMapLayer>("Path");

        var currentTile = towerExclusions.LocalToMap(mousePosition);
        Vector2 tilePosition = towerExclusions.MapToLocal(currentTile);

        bool invalidByExclusion = towerExclusions.GetCellSourceId(currentTile) != -1;
        var pathTile = pathLayer.LocalToMap(mousePosition);
        bool isPathTile = pathLayer.GetCellSourceId(pathTile) != -1;

        bool tileIsOccupied = false;
        foreach (Node child in MapNode.GetNode("Towers").GetChildren())
        {
            if (child is Node2D tower && tower.Position == tilePosition)
            {
                tileIsOccupied = true;
                break;
            }
        }

        bool valid = false;

        if (BuildType.StartsWith("RangedTower"))
        {
            valid = !invalidByExclusion && !isPathTile && !tileIsOccupied;
        }
        else if (BuildType.StartsWith("MeleeTower") || BuildType.StartsWith("Obstacle"))
        {
            valid = isPathTile && !tileIsOccupied;
        }

        if (valid)
        {
            Ui.Call("update_tower_preview", tilePosition, "fff");
            BuildValid = true;
            BuildLocation = tilePosition;
            BuildTile = currentTile;
        }
        else
        {
            Ui.Call("update_tower_preview", tilePosition, "f00");
            BuildValid = false;
        }
    }

    public void CancelBuildMode()
    {
        BuildMode = false;
        BuildValid = false;

        var previewNode = Ui.GetNode("TowerPreview");
        previewNode.QueueFree();
    }

    public void VerifyAndBuild()
    {
        if (BuildValid)
        {
            var scene = GD.Load<PackedScene>("res://Scenes/Towers/" + BuildType + ".tscn");
            var newTower = scene.Instantiate<Node2D>();
            newTower.Position = BuildLocation;

            MapNode.GetNode("Towers").AddChild(newTower);

            BuildMode = false;
            BuildValid = false;
        }
    }
}
