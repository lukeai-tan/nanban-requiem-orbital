using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// The relocate feature is intended for this fight
public partial class Priestess : Boss
{

    public event EventHandler ThreeQ;
    public event EventHandler Half;
    public event EventHandler OneQ;
    public event EventHandler Zero;

    [Export] protected int attack;
    [Export] protected PackedScene projectileScene;
    [Export] protected PackedScene debuffScene;
    [Export] protected PackedScene buffScene;
    [Export] protected PackedScene areaScene;
    protected BasicRangedBuff attack1;
    protected BasicMeleeBuff buff1;
    protected AOEMeleeAttack attack2;
    // Basic Attack
    protected ITargeting<Tower> targeting1;
    // Basic Buff
    protected ITargeting<Enemy> targeting2;
    // Area Nuke
    protected ITargeting<Tower> targeting3;

    protected Vector2 stage = new Vector2(640, 640);
    protected bool onStage = false;
    // OnStage is for activating tileswaps
    public event EventHandler OnStage;
    protected double cooldown = 30;
    public event EventHandler Computation;

    public override void _Ready()
    {
        base._Ready();
        this.skills.Add(this.GetNodeOrNull<BossSkill>("BasicAttack"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("BasicBuff"));
        this.skills.Add(this.GetNodeOrNull<BossSkill>("AreaNuke"));
    }

    public override void SetActions()
    {
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
            this.pathing.Update(this.movementSpeed * (float)delta);
        }
    }

    public override void Act()
    {
        BossSkill skill = base.ChooseSkill();
        skill.Execute();
    }

    // 2 / 3 target ranged attack
    public void BasicAttack()
    {
        this.incapacitated = true;
        foreach (Tower target in this.targeting1.GetTargets(this.range.GetAllTowers()))
        {
            this.attack1.Execute(target);
        }
    }

    // 2 / 3 target enemy buff
    public void BasicBuff()
    {
        this.incapacitated = true;
        foreach (Enemy target in this.targeting2.GetTargets(this.range.GetAllEnemies()))
        {
            this.buff1.Execute(target);
        }
    }

    // Simplifies your UI, so you cant see health bars for some time
    public void UIWipe()
    {
        this.incapacitated = true;
        GD.Print("Wiped");
    }

    // 2 / 4 multi area attack
    public void AreaNuke()
    {
        this.incapacitated = true;
        foreach (Tower target in this.targeting3.GetTargets(this.range.GetAllTowers()))
        {
            this.attack2.Execute(target);
        }
    }

    // Prevents deployment for some time
    public void ClearDeployment()
    {
        this.incapacitated = true;
        GD.Print("Cleared All");
    }

    protected void Recover()
    {
        this.timeSinceLastSkill = 0;
        this.incapacitated = false;
    }

    public override void ReachedObjective(object pathing, EventArgs e)
    {
        this.ToStage();
    }

    protected void ToStage()
    {
        this.pathing = null;
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
        // this.targeting1.NumTagrets(3);
        // this.targeting2.NumTargets(3);
    }

    protected override void OneQF()
    {
        this.OneQ?.Invoke(this, EventArgs.Empty);
        this.skillcooldown = 3;
        this.attack1.SetModifiers(this.attack, 1.5);
        // this.targeting3.NumTargets(4);
    }

    protected override void ZeroF()
    { 
        this.Zero?.Invoke(this, EventArgs.Empty);
    }

}