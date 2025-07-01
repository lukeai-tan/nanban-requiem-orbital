using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GlobalDetectionRange : Node2D
{

    protected Node2D towers;
    protected Node2D enemies;

    public override void _Ready()
    {
        this.towers = this.GetTree().CurrentScene.GetNodeOrNull<Node2D>("GameScene/Map/Towers");
        this.enemies = this.GetTree().CurrentScene.GetNodeOrNull<Node2D>("GameScene/Map/Enemies");
    }

    public List<Tower> GetAllTowers()
    {
        List<Node> list = this.towers.GetChildren().ToList();
        List<Tower> towerlist = new List<Tower>();
        foreach (Node node in list)
        {
            if (node is Tower tower)
            {
                towerlist.Add(tower);
            }
        }
        return towerlist;
    }

    public List<Enemy> GetAllEnemies()
    {
        List<Node> list = this.towers.GetChildren().ToList();
        List<Enemy> enemylist = new List<Enemy>();
        foreach (Node node in list)
        {
            if (node is Enemy enemy)
            {
                enemylist.Add(enemy);
            }
        }
        return enemylist;
    }

}