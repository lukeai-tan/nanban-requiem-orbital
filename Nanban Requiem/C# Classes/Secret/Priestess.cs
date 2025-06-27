using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// The relocate feature is intended for this fight
public partial class Priestess : Boss
{

    // Hp thresholds
    public event EventHandler ThreeQ;
    public event EventHandler Half;
    public event EventHandler OneQ;
    public event EventHandler Zero;

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
    protected Vector2 stage = new Vector2(640, 640);
    protected bool onStage = false;
    public event EventHandler OnStage;
    protected double cooldown = 30;
    public event EventHandler Computation;

    public override void SetActions()
    {
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Inferno"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Cocytus"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("Erebus"));

        this.targeting1 = new PriestessBasic(this);
        this.targeting2 = new PriestessBuff(this);
        this.targeting3 = new PriestessNuke(this);

        this.attack1 = new BasicRangedBuff(this.projectileScene, this, this.debuffScene);
        this.attack1.SetAttackAndSpeed(new ArtsAttack(), 300);
        this.attack1.SetModifiers(this.attack, 0.8);

        this.buff1 = new BasicMeleeBuff(this.buffScene);

        this.attack2 = new AOEMeleeAttack(this.areaScene);
        this.attack2.SetAttack(new PhysicalAttack());
        this.attack2.SetModifiers(this.attack, 0.4);
    }

    public override void _Process(double delta)
    {
        switch (this.timer)
        {
            case >= 60:
                this.ToStage();
                break;
            case >= 45:
                this.GlobalPosition = new Vector2(864, 672);
                break;
            case >= 30:
                this.GlobalPosition = new Vector2(864, 480);
                break;
            case >= 15:
                this.GlobalPosition = new Vector2(416, 481);
                break;
        }
        if (this.onStage)
        {
            if (this.cooldown <= 0)
            {
                this.ComputationMode();
                return;
            }
            this.cooldown -= delta;
        }
        if (!this.incapacitated)
        {
            if (this.timeSinceLastSkill >= 1 / this.skillcooldown)
            {
                this.Act();
            }
            this.timeSinceLastSkill += delta;
        }
        this.timer += delta;
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
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
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
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // Simplifies your UI, so you cant see health bars for some time
    public async void Starfire()
    {
        GD.Print("Starfire");
        this.incapacitated = true;
        // this.animation.Play("");
        this.LockUI?.Invoke(this, EventArgs.Empty);
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // 2 / 4 multi area attack
    public async void Erebus()
    {
        GD.Print("Erebus");
        this.incapacitated = true;
        // this.animation.Play("");
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        foreach (Tower target in this.targeting3.GetTargets(this.range.GetAllTowers()))
        {
            this.attack2.Execute(target);
        }
        await ToSignal(GetTree().CreateTimer(2f), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    // Prevents deployment for some time
    public async void Hyades()
    {
        GD.Print("Hyades");
        this.incapacitated = true;
        // this.animation.Play("");
        this.LockDeployment?.Invoke(this, EventArgs.Empty);
        await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
        this.Recover();
    }

    public async void FatesFinale()
    {
        this.Finale?.Invoke(this, EventArgs.Empty);
        this.invulnerable = true;
        this.incapacitated = true;
        this.targetable = false;
        await ToSignal(GetTree().CreateTimer(30f), SceneTreeTimer.SignalName.Timeout);
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
        this.incapacitated = true;
        this.targetable = false;
        this.Computation?.Invoke(this, EventArgs.Empty);
    }

    public void ExitComputation()
    {
        this.invulnerable = false;
        this.incapacitated = false;
        this.targetable = true;
        this.cooldown = 30;
    }

    protected override void ThreeQF()
    {
        this.ThreeQ?.Invoke(this, EventArgs.Empty);
        this.attack1.SetModifiers(this.attack, 1.2);
        this.attack2.SetModifiers(this.attack, 0.8);
    }

    protected override void HalfF()
    {
        if (!this.onStage)
        {
            this.ToStage();
        }
        this.Half?.Invoke(this, EventArgs.Empty);
        this.targeting1.SetTargets(3);
        this.targeting2.SetTargets(3);
    }

    protected override void OneQF()
    {
        this.OneQ?.Invoke(this, EventArgs.Empty);
        this.skillcooldown = 3;
        this.attack1.SetModifiers(this.attack, 1.5);
        this.targeting3.SetTargets(4);
    }

    protected override void ZeroF()
    {
        this.FatesFinale();
    }

}