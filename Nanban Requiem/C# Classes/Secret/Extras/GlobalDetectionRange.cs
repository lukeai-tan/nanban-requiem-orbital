using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GlobalDetectionRange : Node2D
{

    protected Node2D towers;

    public override void _Ready()
    {
        this.towers = this.GetTree().CurrentScene.GetNodeOrNull<Node2D>("GameScene/Map/Towers");
    }

    public List<Tower> GetAllTowers()
    {
        List<Node> list = this.towers.GetChildren().ToList();
        List<Tower> towerlist = new List<Tower>();
        foreach (Node node in list)
        {
            if (node is Tower tower && tower.CanTarget())
            {
                towerlist.Add(tower);
            }
        }
        return towerlist;
    }

    public List<Enemy> GetAllEnemies()
    {
        Node2D manager = this.GetTree().CurrentScene.GetNodeOrNull<Node2D>("GameScene");
        if (manager is BossStageManager bossManager)
        {
            return bossManager.GetEnemies();
        }
        return [];
    }

}