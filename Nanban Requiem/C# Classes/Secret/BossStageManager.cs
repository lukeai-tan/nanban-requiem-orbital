using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Godot;

public partial class BossStageManager : Node2D
{

    [Signal]
    public delegate void GameFinishedEventHandler(string result);
    protected int objectiveHp = 3000;
    private bool handicap = false;

    private Node towerBuilder;
    private Node towerManager;
    private Node ui;
    private Node buildBar;
    private Node map;
    private Node endGameScreen;
    private Node dpBar;

    private BossWaveManager wave01;
    private BossWaveManager wave02;
    private BossWaveManager wave03;
    private BossWaveManager wave11;
    private BossWaveManager wave12;
    private BossWaveManager wave21;
    private BossWaveManager wave31;
    private BossWaveManager wave32;

    private Priestess priestess;
    private Prts prts;
    private List<Enemy> enemies = [];

    private string gameState = "playing";
    
    [Signal]
    public delegate void GameWonEventHandler();
    [Signal]
    public delegate void GameLostEventHandler();

    public override void _Ready()
    {
        var gameData = GetNode("/root/GameData");
        this.handicap = gameData.Get("boss_map_handicap").AsBool();

        this.ui = GetNode("UI");
        this.buildBar = GetNode("UI/HUD/BuildBar");
        this.dpBar = GetNode("UI/HUD/DPBar");

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
        this.AddChild(towerManager);

        this.towerBuilder.Set("map_node", this.map);
        this.towerBuilder.Set("ui", this.ui);
        this.towerBuilder.Set("tower_manager", this.towerManager);
        this.towerBuilder.Set("tower_exclusions", this.map.GetNodeOrNull<TileMapLayer>("Phase1 Exclusions"));
        this.towerBuilder.Set("low_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase1 Low"));
        this.towerBuilder.Set("high_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase1 High"));
        this.towerBuilder.Set("dp_bar", this.dpBar);

        this.ui.Set("tower_builder", this.towerBuilder);
        this.ui.Set("tower_manager", this.towerManager);
        this.ui.Call("set_hp", this.objectiveHp);

        this.buildBar.Call("setup", this.towerBuilder);
        this.buildBar.Set("tower_builder", this.towerBuilder);

        this.towerManager.Call("set_ui", this.ui);
        this.towerManager.Set("map_node", this.map);
        this.towerManager.Connect("tower_count_changed", new Callable(this.ui, "update_tower_count"));
        this.towerManager.Call("change_deployment", 8);

        if (this.handicap)
        {
            this.objectiveHp = 4000;

            this.wave01 = this.map.GetNodeOrNull<BossWaveManager>("Wave01");
            this.wave01.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "Samurai"], [5.0, 15.0, 5.0, 5.0, 15.0]);

            this.wave02 = this.map.GetNodeOrNull<BossWaveManager>("Wave02");
            this.wave02.SetWave(["Samurai", "Samurai", "WhiteSamurai", "Samurai"], [5.0, 15.0, 5.0, 15.0]);

            this.wave03 = this.map.GetNodeOrNull<BossWaveManager>("Wave03");
            this.wave03.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai"], [5.0, 15.0, 5.0, 15.0]);

            this.wave11 = this.map.GetNodeOrNull<BossWaveManager>("Wave11");
            this.wave11.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "Samurai"], [5.0, 15.0, 5.0, 5.0, 15.0]);

            this.wave12 = this.map.GetNodeOrNull<BossWaveManager>("Wave12");
            this.wave12.SetWave(["Samurai", "Samurai", "WhiteSamurai", "Samurai", "Samurai"], [5.0, 15.0, 5.0, 5.0, 15.0]);

            this.wave21 = this.map.GetNodeOrNull<BossWaveManager>("Wave21");
            this.wave21.SetWave(["Samurai", "Samurai", "RocketSamurai", "WhiteSamurai", "Samurai", "Samurai"], [5.0, 10.0, 10.0, 5.0, 5.0, 10.0]);

            this.wave31 = this.map.GetNodeOrNull<BossWaveManager>("Wave31");
            this.wave31.SetWave(["DemonSlime"], [0]);
            this.wave31.Auto(false);

            this.wave32 = this.map.GetNodeOrNull<BossWaveManager>("Wave32");
            this.wave32.SetWave(["NightBorne"], [0]);
            this.wave32.Auto(false);
        }
        else
        {
            this.wave01 = this.map.GetNodeOrNull<BossWaveManager>("Wave01");
            this.wave01.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "RocketSamurai"], [5.0, 10.0, 5.0, 5.0, 10.0]);

            this.wave02 = this.map.GetNodeOrNull<BossWaveManager>("Wave02");
            this.wave02.SetWave(["Samurai", "Samurai", "WhiteSamurai", "Samurai", "RocketSamurai"], [5.0, 10.0, 5.0, 10.0, 15.0]);

            this.wave03 = this.map.GetNodeOrNull<BossWaveManager>("Wave03");
            this.wave03.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "WhiteSamurai"], [5.0, 10.0, 5.0, 10.0, 15.0]);

            this.wave11 = this.map.GetNodeOrNull<BossWaveManager>("Wave11");
            this.wave11.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "Samurai", "WhiteSamurai"], [5.0, 10.0, 5.0, 5.0, 10.0, 15.0]);

            this.wave12 = this.map.GetNodeOrNull<BossWaveManager>("Wave12");
            this.wave12.SetWave(["Samurai", "Samurai", "WhiteSamurai", "Samurai", "Samurai", "RocketSamurai"], [5.0, 10.0, 5.0, 5.0, 10.0, 15.0]);

            this.wave21 = this.map.GetNodeOrNull<BossWaveManager>("Wave21");
            this.wave21.SetWave(["Samurai", "Samurai", "RocketSamurai", "Samurai", "WhiteSamurai", "Samurai", "RocketSamurai", "Samurai"], [5.0, 5.0, 5.0, 10.0, 5.0, 5.0, 5.0, 10.0]);

            this.wave31 = this.map.GetNodeOrNull<BossWaveManager>("Wave31");
            this.wave31.SetWave(["DemonSlime", "NightBorne"], [15.0, 0]);
            this.wave31.Auto(false);

            this.wave32 = this.map.GetNodeOrNull<BossWaveManager>("Wave32");
            this.wave32.SetWave(["NightBorne", "DemonSlime"], [15.0, 0]);
            this.wave32.Auto(false);
        }
        
        endGameScreen = GetNode("UI/EndGameScreen");
        Connect("GameWon", new Callable(endGameScreen, "_on_game_won"));
        Connect("GameLost", new Callable(endGameScreen, "_on_game_lost"));

    }

    public void AddEnemy(Enemy enemy)
    {
        this.enemies.Add(enemy);
        enemy.Despawning += this.RemoveEnemy;
    }

    protected void RemoveEnemy(object obj, EventArgs e)
    {
        if (obj is Enemy enemy)
        {
            this.enemies.Remove(enemy);
        }
    }

    public List<Enemy> GetEnemies()
    {
        return this.enemies;
    }

    public void Initialize()
    {
        this.GetPrts();
        this.SpawnPriestess();
        this.wave01.Activate();
        this.wave02.Activate();
        this.wave03.Activate();
    }

    private void GetPrts()
    {
        this.prts = this.map.GetNodeOrNull<Prts>("Prts");
        this.prts.SetHealthBar((TextureProgressBar)this.ui.Call("get_prts_healthbar"));
        this.prts.SetShieldBar((TextureProgressBar)this.ui.Call("get_prts_shieldbar"));
        this.prts.SetActions();
        this.prts.Corrode += (object boss, EventArgs e) => this.Corrode();
        this.prts.Zero += (object boss, EventArgs e) => this.SwapPriestess();
    }

    private void Corrode(int damage)
    {
        if (gameState == "defeat" || gameState == "victory")
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
        if (gameState == "defeat" || gameState == "victory")
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
        this.priestess.Computation += (object boss, EventArgs e) => this.SwapPrts();
        this.priestess.OnStage += (object boss, EventArgs e) => this.PhaseTwo();
        this.priestess.LockUI += (object boss, EventArgs e) => this.InvertUI();
        this.priestess.LockDeployment += (object boss, EventArgs e) => this.LockDeployment();
        this.priestess.Finale += (object boss, EventArgs e) => this.PhaseFinal();
        this.priestess.Zero += (object boss, EventArgs e) =>
        {
            if (gameState != "defeat")
            {
                gameState = "victory";
                this.EmitSignal(SignalName.GameWon); // new one, idk if it works 
            }
        };
    }

    private async void PhaseTwo()
    {
        this.GetTree().Paused = true;
        this.wave01.Deactivate();
        this.wave02.Deactivate();
        this.wave03.Deactivate();
        this.ui.Call("toggle_ui");

        List<Enemy> currlist = this.enemies;
        this.enemies = [];
        foreach (Enemy enemy in currlist)
        {
            enemy.Despawn();
        }
        List<Node> towers = this.map.GetNodeOrNull("Towers").GetChildren().ToList();
        foreach (Tower tower in towers)
        {
            tower.Despawn();
        }

        this.map.GetNodeOrNull<TileMapLayer>("Phase1 Low").Visible = false;
        this.map.GetNodeOrNull<TileMapLayer>("Phase1 High").Visible = false;
        this.map.GetNodeOrNull<TileMapLayer>("Phase2 Low").Visible = true;
        this.map.GetNodeOrNull<TileMapLayer>("Phase2 High").Visible = true;
        this.towerBuilder.Set("tower_exclusions", this.map.GetNodeOrNull<TileMapLayer>("Phase2 Exclusions"));
        this.towerBuilder.Set("low_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase2 Low"));
        this.towerBuilder.Set("high_ground", this.map.GetNodeOrNull<TileMapLayer>("Phase2 High"));

        await ToSignal(GetTree().CreateTimer(3f, true, false, true), SceneTreeTimer.SignalName.Timeout);
        this.ui.Call("toggle_ui");
        this.GetTree().Paused = false;
    }

    private void SwapPrts()
    {
        this.ui.Call("update_ui");
        this.wave11.Deactivate();
        this.wave12.Deactivate();
        this.wave21.Activate();
    }

    private void SwapPriestess()
    {
        this.ui.Call("update_ui");
        this.wave21.Deactivate();
        this.wave11.Activate();
        this.wave12.Activate();
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
        this.wave31.Activate();
        this.wave32.Activate();
    }

    private void OnWaveComplete()
    {
        //this.waveSpawner.Call("start_next_wave");
    }

    public void OnBaseDamage(float damage)
    {
        if (this.handicap)
        {
            this.Corrode((int)damage * 100);
        }
        else
        {
            this.Corrode((int)damage * 200);
        }
    }

    public override void _Process(double delta)
    {
        if ((bool)this.towerBuilder.Get("build_mode"))
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