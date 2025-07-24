using System;
using System.Collections.Generic;
using Godot;

public partial class BossWaveManager : Node2D
{
    protected BossStageManager manager;
    protected bool active = false;
    protected bool auto = true;
    [Export] protected Path2D path;
    protected bool complete = true;
    protected List<String> enemies = [];
    protected List<double> intervals = [];

    public override void _Ready()
    {
        this.manager = this.GetParent().GetParentOrNull<BossStageManager>();
    }

    public void SetWave(List<String> enemies, List<double> intervals)
    {
        this.enemies = enemies;
        this.intervals = intervals;
    }

    public void Auto(bool auto)
    {
        this.auto = auto;
    }

    public void Activate()
    {
        this.active = true;
    }

    public void Deactivate()
    {
        this.active = false;
    }

    public override void _Process(double delta)
    {
        if (this.active)
        {
            if (this.complete && this.path.GetChildCount() == 0)
            {
                this.complete = false;
                this.StartWave();
            }
        }
    }

    public async void StartWave()
    {
        int i = 0;
        while (this.active && i < this.enemies.Count)
        {
            Node entity = ((PackedScene)GD.Load("res://Scenes/Enemies/" + this.enemies[i] + ".tscn")).Instantiate();
            if (entity is Enemy enemy)
            {
                enemy.Initialize(this.path);
                enemy.Connect("DamageBase", new Callable(this, nameof(OnBaseDamage)));
                this.manager.AddEnemy(enemy);
            }
            await ToSignal(GetTree().CreateTimer(this.intervals[i], false), SceneTreeTimer.SignalName.Timeout);
            i++;
        }
        if (!this.auto)
        {
            this.Deactivate();
        }
        this.complete = true;
    }

    public void OnBaseDamage(float damage)
    {
        this.manager.OnBaseDamage(damage);
    }

}

