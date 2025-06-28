using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class BossStageManager : Node2D
{

    [Signal]
    public delegate void GameFinishedEventHandler(string result);
    protected float baseHp = 30;
    protected TextureProgressBar corrosionBar;
    protected int corrosion = 8000;

    private Node towerBuilder;
    private BossWaveSpawner waveSpawner;
    private Node towerManager;
    private Node ui;
    private Node buildBar;
    private Node map;

    private Priestess priestess;
    private Prts prts;

    public override void _Ready()
    {
        this.ui = GetNode("UI");
        this.buildBar = GetNode("UI/HUD/BuildBar");

        var mapScene = GD.Load<PackedScene>($"res://Scenes/Maps/BossMap.tscn");
        Node mapNode = mapScene.Instantiate();
        mapNode.Name = "Map";
        this.AddChild(mapNode);
        this.map = mapNode;

        var towerBuilderScript = GD.Load<Script>("res://Scenes/MainScenes/TowerBuilder.gd");
        this.towerBuilder = (Node)towerBuilderScript.Call("new");

        var towerManagerScript = GD.Load<Script>("res://Scenes/MainScenes/TowerManager.gd");
        this.towerManager = (Node)towerManagerScript.Call("new");

        this.AddChild(towerBuilder);
        //this.AddChild(waveSpawner);
        this.AddChild(towerManager);

        this.towerBuilder.Set("map_node", this.map);
        this.towerBuilder.Set("ui", this.ui);
        this.towerBuilder.Set("tower_manager", this.towerManager);

        this.ui.Set("tower_builder", this.towerBuilder);
        this.ui.Set("tower_manager", this.towerManager);

        this.buildBar.Call("setup", this.towerBuilder);
        this.buildBar.Set("tower_builder", this.towerBuilder);

        this.towerManager.Call("set_ui", this.ui);
        this.towerManager.Set("map_node", this.map);
        this.towerManager.Connect("tower_count_changed", new Callable(this.ui, "update_tower_count"));

        //this.waveSpawner.Set("map_node", mapNode);
        //this.waveSpawner.Set("map_to_load", MapToLoad);
        //this.waveSpawner.Connect("wave_complete", new Callable(this, nameof(OnWaveComplete)));
        //this.waveSpawner.Call("start_next_wave");
    }

    public void Initialize()
    {
        this.GetPrts();
        this.SpawnPriestess();
        this.corrosionBar = this.map.GetNodeOrNull<TextureProgressBar>("CorrosionBar");
        this.corrosionBar.MaxValue = this.corrosion;
        this.corrosionBar.Value = this.corrosion;
    }

    private void GetPrts()
    {
        this.prts = this.map.GetNodeOrNull<Prts>("Prts");
        this.prts.SetActions();
        this.prts.Corrode += (object boss, EventArgs e) => this.Corrode();
        this.prts.Zero += (object boss, EventArgs e) => this.ResetCorrosion();
    }

    private void Corrode()
    {
        if (this.corrosion == 0)
        {
            this.EmitSignal(SignalName.GameFinished, "game_finished");
        }
        else
        {
            this.corrosion -= 1;
            this.corrosionBar.Value = this.corrosion;
        }
    }

    private void ResetCorrosion()
    {
        this.corrosion = 8000;
        this.corrosionBar.Value = this.corrosion;
    }

    private void SpawnPriestess()
    {
        this.priestess = GD.Load<PackedScene>("res://Scenes/Bosses/Priestess.tscn").Instantiate<Priestess>();
        this.map.AddChild(this.priestess);
        this.priestess.GlobalPosition = new Vector2(418, 542);
        this.priestess.SetActions();
        this.prts.Connect(this.priestess);
        this.priestess.OnStage += (object boss, EventArgs e) => this.PhaseTwo();
        this.priestess.Finale += (object boss, EventArgs e) => this.PhaseFinal();
        this.priestess.Zero += (object boss, EventArgs e) => this.EmitSignal(SignalName.GameFinished, "victory");
    }

    private void PhaseTwo()
    {
        List<Node> towers = this.map.GetNodeOrNull("Towers").GetChildren().ToList();
        List<Node> enemies = this.map.GetNodeOrNull("Enemies").GetChildren().ToList();
        foreach (Node node in enemies)
        {
            node.QueueFree();
        }
        foreach (Node node in towers)
        {
            node.QueueFree();
        }
    }

    private void PhaseFinal()
    {
        
    }

    private void OnWaveComplete()
    {
        //this.waveSpawner.Call("start_next_wave");
    }

    public void OnBaseDamage(float damage)
    {
        this.baseHp -= damage;
        this.ui.Call("update_health_bar", this.baseHp);

        if (this.baseHp <= 0)
            this.EmitSignal(SignalName.GameFinished, "game_finished");
    }

    public override void _Process(double delta)
    {
        if ((bool) this.towerBuilder.Get("build_mode"))
            this.towerBuilder.Call("update_tower_preview");

        //this.waveSpawner.Call("_process", delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("ui_cancel") && (bool) this.towerBuilder.Get("build_mode"))
        {
            this.towerBuilder.Call("cancel_build_mode");
        }

        if (@event.IsActionReleased("ui_accept") && (bool) towerBuilder.Get("build_mode"))
        {
            this.towerBuilder.Call("verify_and_build");
            this.towerBuilder.Call("cancel_build_mode");
        }
    }

}