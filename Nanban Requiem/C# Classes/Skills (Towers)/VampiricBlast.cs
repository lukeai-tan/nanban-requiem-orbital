using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;

public partial class VampiricBlast : TowerSkill
{

    [Export] protected int damage;
    [Export] protected double multiplier;
    [Export] protected PackedScene projectileScene;
    [Export] protected PackedScene aoeScene;
    [Export] protected PackedScene dotScene;
    [Export] protected PackedScene regenScene;
    protected ITargeting<Enemy> targeting = new EnemyClosestToBase();
    protected DetectionRange<Enemy> range;

    public override void _Ready()
    {
        base._Ready();
        this.range = this.owner.GetNodeOrNull<DetectionRange<Enemy>>("DetectionRange");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (this.IsReady())
        {
            this.Call();
        }
    }

    public override void Call()
    {
        if (this.IsReady())
        {
            AOERangedAttack aoe = new AOERangedAttack(this.projectileScene, this.owner, this.aoeScene);
            aoe.SetAttackAndSpeed(new ArtsAttack(), 300);
            aoe.SetModifiers(this.damage, this.multiplier);
            Enemy target = this.targeting.GetTarget(this.range.GetTargets());
            if (target != null)
            {
                aoe.Execute(target);
            }

            BasicMeleeBuff regen = new BasicMeleeBuff(this.regenScene);
            regen.SetAttack(new Heal());
            regen.Execute(this.owner);

            this.points = this.initialPoints;
        }
    }

}