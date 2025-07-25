using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// The relocate feature is intended for this fight
public partial class Priestess : Boss
{

    // Hp thresholds
    public event EventHandler Active;
    public event EventHandler ThreeQ;
    public event EventHandler Half;
    public event EventHandler OneQ;
    public event EventHandler Zero;
    public bool active = true;
    private bool handicap = false;

    // Skills
    [Export] protected PackedScene projectileScene;
    [Export] protected PackedScene debuffScene;
    [Export] protected PackedScene buffScene;
    [Export] protected PackedScene areaScene;
    protected BasicRangedBuff attack1;
    protected BasicMeleeBuff buff1;
    protected AOEMeleeAttack attack2;
    protected PriestessBasic targeting1;
    protected PriestessBuff targeting2;
    protected PriestessNuke targeting3;
    public event EventHandler LockUI;
    public event EventHandler LockDeployment;
    public event EventHandler Finale;

    // Phase transition 
    protected double timer = 0;
    protected double idle = 30;
    protected int teleports = 0;
    protected Vector2 stage = new Vector2(640, 640);
    protected bool onStage = false;
    public event EventHandler OnStage;
    protected double cooldown = 0;
    public event EventHandler Computation;

    public override void _Ready()
    {
        var gameData = GetNode("/root/GameData");
        this.handicap = gameData.Get("boss_map_handicap").AsBool();
        if (this.handicap)
        {
            this.health = 20000;
            this.attack = 160;
            this.idle = 20;
        }
        base._Ready();
    }

    public override void SetActions()
    {
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Inferno"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Cocytus"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Erebus"));

        this.targeting1 = new PriestessBasic(this);
        this.targeting2 = new PriestessBuff(this);
        this.targeting3 = new PriestessNuke(this);
        this.targeting1.SetTargets(2);
        this.targeting2.SetTargets(2);
        this.targeting3.SetTargets(2);

        this.attack1 = new BasicRangedBuff(this.projectileScene, this, this.debuffScene);
        this.attack1.SetAttackAndSpeed(new ArtsAttack(), 300);

        this.buff1 = new BasicMeleeBuff(this.buffScene);
        this.buff1.SetAttack(new Heal());

        this.attack2 = new AOEMeleeAttack(this.areaScene);
        this.attack2.SetAttack(new ArtsAttack());

        this.attack1.SetModifiers(this.attack, 1);
        this.buff1.SetModifiers(this.attack, 1);
        this.attack2.SetModifiers(this.attack, 0.8);
    }

    public override void _Process(double delta)
    {
        if (this.active && !this.incapacitated)
        {
            if (this.onStage)
            {
                if (this.cooldown <= 0)
                {
                    this.ComputationMode();
                    return;
                }
                this.cooldown -= delta;
            }
            else
            {
                this.timer += delta;
                if (timer >= this.idle)
                {
                    switch (this.teleports)
                    {
                        case 3:
                            this.ToStage();
                            return;
                        case 2:
                            this.GlobalPosition = new Vector2(737, 542);
                            this.timer = 0;
                            this.teleports += 1;
                            return;
                        case 1:
                            this.GlobalPosition = new Vector2(546, 542);
                            this.timer = 0;
                            this.teleports += 1;
                            return;
                        case 0:
                            this.GlobalPosition = new Vector2(865, 542);
                            this.timer = 0;
                            this.teleports += 1;
                            return;
                    }
                }
            }
            if (this.timeSinceLastSkill >= this.skillcooldown)
            {
                this.Act();
            }
            this.timeSinceLastSkill += delta;
        }
    }

    // 2 / 3 target ranged attack
    public async void Inferno()
    {
        GD.Print("Inferno");
        this.incapacitated = true;
        // this.animation.Play("");
        foreach (Tower target in this.targeting1.GetTargets(this.range.GetAllTowers()))
        {
            this.attack1.Execute(target);
        }
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // 2 / 3 target enemy buff
    public async void Cocytus()
    {
        GD.Print("Cocytus");
        this.incapacitated = true;
        // this.animation.Play("");
        foreach (Enemy target in this.targeting2.GetTargets(this.range.GetAllEnemies()))
        {
            this.buff1.Execute(target);
        }
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // Simplifies your UI, so you cant see health bars for some time
    public async void Starfire()
    {
        GD.Print("Starfire");
        this.incapacitated = true;
        // this.animation.Play("");
        this.LockUI?.Invoke(this, EventArgs.Empty);
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // 2 / 4 multi area attack
    public async void Erebus()
    {
        GD.Print("Erebus");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(1f, false), SceneTreeTimer.SignalName.Timeout);
        foreach (Tower target in this.targeting3.GetTargets(this.range.GetAllTowers()))
        {
            this.attack2.Execute(target);
        }
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // Prevents deployment for some time
    public async void Hyades()
    {
        GD.Print("Hyades");
        this.incapacitated = true;
        // this.animation.Play("");
        this.LockDeployment?.Invoke(this, EventArgs.Empty);
        await ToSignal(GetTree().CreateTimer(0.5f, false), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    public async void FatesFinale()
    {
        this.Finale?.Invoke(this, EventArgs.Empty);
        this.invulnerable = true;
        this.active = false;
        this.targetable = false;

        Rect2 viewport = GetViewport().GetVisibleRect();
        float minX = viewport.Position.X;
        float maxX = viewport.End.X;
        float minY = viewport.Position.Y;
        float maxY = viewport.End.Y;
        Node2D projectilesNode = this.GetTree().CurrentScene.GetNode<Node2D>("GameScene/Map/Projectiles");

        int attacks = 240;
        if (this.handicap)
        {
            attacks = 180;
            this.attack2.SetModifiers(this.attack, 0.5);
        }
        else
        {
            this.attack2.SetModifiers(this.attack, 0.7);
        }

        for (int i = 0; i < attacks; i++)
        {
            Vector2 position = new Vector2((float)GD.RandRange(minX, maxX), (float)GD.RandRange(minY, maxY));
            this.attack2.Execute<Tower>(position, projectilesNode);
            await ToSignal(GetTree().CreateTimer(0.25f, false), SceneTreeTimer.SignalName.Timeout);
        }

        this.Zero?.Invoke(this, EventArgs.Empty);
        this.QueueFree();
    }

    protected void Recover()
    {
        this.timeSinceLastSkill = 0;
        this.incapacitated = false;
    }

    protected void ToStage()
    {
        this.GlobalPosition = this.stage;
        this.onStage = true;
        this.OnStage?.Invoke(this, EventArgs.Empty);
    }

    // When priestess switches to prts
    public void ComputationMode()
    {
        this.invulnerable = true;
        this.active = false;
        this.targetable = false;
        this.Computation?.Invoke(this, EventArgs.Empty);
    }

    public void ExitComputation()
    {
        this.Active?.Invoke(this, EventArgs.Empty);
        this.invulnerable = false;
        this.active = true;
        this.targetable = true;
        this.cooldown = 90;
    }

    protected override void ThreeQF()
    {
        this.ThreeQ?.Invoke(this, EventArgs.Empty);
        this.attack1.SetModifiers(this.attack, 1.1);
        this.attack2.SetModifiers(this.attack, 0.9);
        this.targeting2.SetTargets(3);
    }

    protected override void HalfF()
    {
        if (!this.onStage)
        {
            this.ToStage();
        }
        this.Half?.Invoke(this, EventArgs.Empty);
        this.attack1.SetModifiers(this.attack, 1.2);
        this.targeting1.SetTargets(3);
        if (!this.handicap)
        {
            this.skillcooldown = 2.5;
            this.targeting3.SetTargets(3);
        }   
    }

    protected override void OneQF()
    {
        this.OneQ?.Invoke(this, EventArgs.Empty);
        if (!this.handicap)
        {
            this.skillcooldown = 2;
            this.targeting3.SetTargets(4);
            this.attack1.SetModifiers(this.attack, 1.3);
        }
        else
        {
            this.targeting3.SetTargets(3);
        }
        this.attack2.SetModifiers(this.attack, 1);
    }

    protected override void ZeroF()
    {
        this.FatesFinale();
    }

}