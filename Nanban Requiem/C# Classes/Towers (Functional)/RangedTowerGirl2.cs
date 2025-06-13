using System;
using System.Collections.Generic;
using Godot;

public partial class RangedTowerGirl2 : RangedDOTBase, ITowerSkill
{

    [Export] protected PackedScene aoeScene;
    protected VampiricBlast sequence;
    protected TowerSkill skill = new TowerSkill(10, 0);

    public override void _Ready()
    {
        this.rangedAttack = new ArtsAttack();
        this.targeting = new EnemyClosestToBase();
        base._Ready();
        this.sequence = new VampiricBlast(this.projectileScene, this.aoeScene, this.dotScene, this); 
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        this.skill.gainPoints(delta);
        if (this.skill.IsReady())
        {
            this.Call();
        }
    }

    public bool IsReady()
    {
        return skill.IsReady();
    }

    public void Call()
    {
        Enemy target = this.targeting.GetTarget(this.range.GetTargets());
        this.skill.Call(new List<Unit> { target, target, this }, this.sequence.Skill(this.rangedDamage, 2));
    }

    public override string ToString()
    {
        return "Ranged Tower Girl 2: " + this.rangedDamage.ToString() + " " + base.ToString();
    }

}