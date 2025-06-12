using Godot;
using System;

public partial class GameScene : Node2D
{
    [Signal]
    public delegate void GameFinishedEventHandler(string result);

    private TowerBuilder towerBuilder;
    private WaveSpawner waveSpawner;

    private Control ui;
    private float baseHealth = 5.0f;

    public override void _Ready()
    {
        ui = GetNode<Control>("UI");

        // Instantiate TowerBuilder and WaveSpawner directly
        towerBuilder = new TowerBuilder();
        waveSpawner = new WaveSpawner();

        AddChild(towerBuilder);
        AddChild(waveSpawner);

        var mapNode = GetNode<Node2D>("Map1");

        // Assign references
        towerBuilder.MapNode = mapNode;
        towerBuilder.Ui = ui;
        ui.Set("tower_builder", towerBuilder);

        waveSpawner.MapNode = mapNode;
        waveSpawner.WaveComplete += OnWaveComplete;

        foreach (var node in GetTree().GetNodesInGroup("tower_options"))
        {
            if (node is Button button)
            {
                button.Pressed += () => towerBuilder.InitiateBuildMode(button.Name);
            }
        }

        waveSpawner.StartNextWave();
    }

    private void OnWaveComplete()
    {
        waveSpawner.StartNextWave();
    }

    public void OnBaseDamage(float damage)
    {
        baseHealth -= damage;
        ui.Call("update_health_bar", baseHealth);

        if (baseHealth <= 0)
        {
            EmitSignal(SignalName.GameFinished, "game_finished");
        }
    }

    public override void _Process(double delta)
    {
        if (towerBuilder.BuildMode)
        {
            towerBuilder.UpdateTowerPreview();
        }

        waveSpawner._Process(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionReleased("ui_cancel") && towerBuilder.BuildMode)
        {
            towerBuilder.CancelBuildMode();
        }

        if (@event.IsActionReleased("ui_accept") && towerBuilder.BuildMode)
        {
            towerBuilder.VerifyAndBuild();
            towerBuilder.CancelBuildMode();
        }
    }
}
