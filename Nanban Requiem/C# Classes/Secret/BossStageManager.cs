using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

public partial class BossStageManager : Node2D
{

    [Signal]
    public delegate void GameFinishedEventHandler(string result);
    protected int objectiveHp = 40000;

    private Node towerBuilder;
    private BossWaveSpawner waveSpawner;
    private Node towerManager;
    private Node ui;
    private Node buildBar;
    private Node map;
    private Node endGameScreen;

    private Priestess priestess;
    private Prts prts;

    private string gameState = "playing";
    
    [Signal]
    public delegate void GameWonEventHandler();
    [Signal]
    public delegate void GameLostEventHandler();
    
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
        this.towerBuilder.Set("tower_exclusions", this.map.GetNodeOrNull<TileMapLayer>("Phase1 Exclusions"));
        this.towerBuilder.Set("low_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase1 Low"));
        this.towerBuilder.Set("high_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase1 High"));

        this.ui.Set("tower_builder", this.towerBuilder);
        this.ui.Set("tower_manager", this.towerManager);
        this.ui.Call("set_hp", this.objectiveHp);

        this.buildBar.Call("setup", this.towerBuilder);
        this.buildBar.Set("tower_builder", this.towerBuilder);

        this.towerManager.Call("set_ui", this.ui);
        this.towerManager.Set("map_node", this.map);
        this.towerManager.Connect("tower_count_changed", new Callable(this.ui, "update_tower_count"));

        //this.waveSpawner.Set("map_node", mapNode);
        //this.waveSpawner.Set("map_to_load", MapToLoad);
        //this.waveSpawner.Connect("wave_complete", new Callable(this, nameof(OnWaveComplete)));
        //this.waveSpawner.Call("start_next_wave");
        
        endGameScreen = GetNode("UI/EndGameScreen");
        Connect("GameWon", new Callable(endGameScreen, "_on_game_won"));
        Connect("GameLost", new Callable(endGameScreen, "_on_game_lost"));
        
    }

    public void Initialize()
    {
        this.GetPrts();
        this.SpawnPriestess();
    }

    private void GetPrts()
    {
        this.prts = this.map.GetNodeOrNull<Prts>("Prts");
        this.prts.SetHealthBar((TextureProgressBar) this.ui.Call("get_prts_healthbar"));
        this.prts.SetActions();
        this.prts.Corrode += (object boss, EventArgs e) => this.Corrode();
        this.prts.Zero += (object boss, EventArgs e) => this.ui.Call("update_ui");
    }

    private void Corrode(int damage)
    {
        if (gameState == "defeat")
            return;
        if (this.objectiveHp <= damage)
        {
            // this.EmitSignal(SignalName.GameFinished, "game_finished");
            gameState = "defeat";
            this.EmitSignal(SignalName.GameLost);
        }
        else
        {
            this.objectiveHp -= damage;
            this.ui.Call("update_health_bar", this.objectiveHp);
        }
    }

    private void Corrode()
    {
        if (gameState == "defeat")
            return;
        if (this.objectiveHp <= 1)
        {
            //this.EmitSignal(SignalName.GameFinished, "game_finished");
            gameState = "defeat";
            this.EmitSignal(SignalName.GameLost);
        }
        else
        {
            this.objectiveHp -= 1;
            this.ui.Call("corrode");
        }
    }

    private void SpawnPriestess()
    {
        this.priestess = GD.Load<PackedScene>("res://Scenes/Bosses/Priestess.tscn").Instantiate<Priestess>();
        this.map.AddChild(this.priestess);
        this.priestess.GlobalPosition = new Vector2(418, 542);
        this.priestess.SetHealthBar((TextureProgressBar)this.ui.Call("get_priestess_healthbar"));
        this.priestess.SetActions();
        this.prts.Connect(this.priestess);
        this.priestess.Computation += (object boss, EventArgs e) => this.ui.Call("update_ui");
        this.priestess.OnStage += (object boss, EventArgs e) => this.PhaseTwo();
        this.priestess.LockUI += (object boss, EventArgs e) => this.InvertUI();
        this.priestess.LockDeployment += (object boss, EventArgs e) => this.LockDeployment();
        this.priestess.Finale += (object boss, EventArgs e) => this.PhaseFinal();
        //this.priestess.Zero += (object boss, EventArgs e) => this.EmitSignal(SignalName.GameFinished, "victory");
        this.priestess.Zero += (object boss, EventArgs e) => this.EmitSignal(SignalName.GameWon); // new one, idk if it works
    }

    private async void PhaseTwo()
    {
        this.GetTree().Paused = true;
        this.ui.Call("toggle_ui");
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

        this.map.GetNodeOrNull<TileMapLayer>("Phase1 Low").Visible = false;
        this.map.GetNodeOrNull<TileMapLayer>("Phase1 High").Visible = false;
        this.map.GetNodeOrNull<TileMapLayer>("Phase2 Low").Visible = true;
        this.map.GetNodeOrNull<TileMapLayer>("Phase2 High").Visible = true;
        this.towerBuilder.Set("tower_exclusions", this.map.GetNodeOrNull<TileMapLayer>("Phase2 Exclusions"));
        this.towerBuilder.Set("low_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase2 Low"));
        this.towerBuilder.Set("high_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase2 High"));

        await ToSignal(GetTree().CreateTimer(3f), SceneTreeTimer.SignalName.Timeout);
        this.ui.Call("toggle_ui");
        this.GetTree().Paused = false;
    }

    private async void InvertUI()
    {
        Transform2D invert = new Transform2D(new Vector2(-1, 0), new Vector2(0, -1), GetViewport().GetVisibleRect().Size);
        this.ui.Set("transform", invert);
        await ToSignal(GetTree().CreateTimer(15f, false), SceneTreeTimer.SignalName.Timeout);
        this.ui.Set("transform", Transform2D.Identity);
    }

    private async void LockDeployment()
    {
        this.ui.Call("disable_build_bar");
        await ToSignal(GetTree().CreateTimer(15f, false), SceneTreeTimer.SignalName.Timeout);
        this.ui.Call("enable_build_bar");
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
        this.Corrode((int)damage * 1000);
    }

    public override void _Process(double delta)
    {
        if ((bool) this.towerBuilder.Get("build_mode"))
            this.towerBuilder.Call("update_tower_preview");

        //this.waveSpawner.Call("_process", delta);
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("ui_cancel") && (bool)this.towerBuilder.Get("build_mode"))
        {
            this.towerBuilder.Call("cancel_build_mode");
        }

        if (@event.IsActionReleased("ui_accept") && (bool)towerBuilder.Get("build_mode"))
        {
            this.towerBuilder.Call("verify_and_build");
            this.towerBuilder.Call("cancel_build_mode");
        }
    }

}