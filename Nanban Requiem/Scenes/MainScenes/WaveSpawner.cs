using Godot;
using System;
using System.Threading.Tasks;

public partial class WaveSpawner : Node2D
{
    [Signal]
    public delegate void WaveCompleteEventHandler();

    private int currentWave = 0;
    private int enemiesInWave = 0;
    private bool allEnemiesInWaveSpawned = false;

    public Node2D MapNode;

    public override void _Process(double delta)
    {
        var path = MapNode.GetNode<Node>("Path2D");
        if (allEnemiesInWaveSpawned && path.GetChildCount() == 0)
        {
            allEnemiesInWaveSpawned = false;
            EmitSignal(SignalName.WaveComplete);
        }
    }

    public async void StartNextWave()
    {
        var waveData = RetrieveWaveData();
        await ToSignal(GetTree().CreateTimer(0.2), "timeout");
        await SpawnEnemies(waveData);
    }

    private Godot.Collections.Array RetrieveWaveData()
    {
        var gameData = (Node)GetNode("/root/GameData");
        var waveDataMap = (Godot.Collections.Dictionary)gameData.Get("wave_data");

        currentWave += 1;
        var waveData = (Godot.Collections.Array)waveDataMap["Map1"];
        enemiesInWave = waveData.Count;

        return waveData;
    }

    private async Task SpawnEnemies(Godot.Collections.Array waveData)
    {
        foreach (Godot.Collections.Array i in waveData)
        {
            await ToSignal(GetTree().CreateTimer((float)i[1]), "timeout");

            var enemyScene = GD.Load<PackedScene>("res://Scenes/Enemies/" + (string)i[0] + ".tscn");
            var enemy = enemyScene.Instantiate<Node>();

            enemy.Call("Initialize", MapNode.GetNode("Path2D"));
            enemy.Connect("DamageBase", new Callable(GetParent(), "on_base_damage"));

            MapNode.GetNode("Path2D").AddChild(enemy);
        }

        allEnemiesInWaveSpawned = true;
    }
}