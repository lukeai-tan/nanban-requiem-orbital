using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

// The relocate feature is intended for this fight
public partial class Priestess : Enemy
{

    protected List<BossSkill> regularSkills;
    protected List<BossSkill> modeSkills;
    protected bool computationMode = false;
    protected bool invulnerable = true;
    protected bool incapacitated = false;

    public override void _Ready()
    {
        base._Ready();

        // DOT + attack down ranged attack
        this.regularSkills.Add(this.GetNodeOrNull<BossSkill>("Attack"));

        // Buffs two enemy units
        this.regularSkills.Add(this.GetNodeOrNull<BossSkill>("Buff"));

        // Clears all enemies, towers and inverts all ranged/melee tiles
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("TileInvert"));

        // Simplifies your UI, so you cant see health bars for some time
        this.regularSkills.Add(this.GetNodeOrNull<BossSkill>("CodeAbstraction"));

        // Global multi area attacks
        this.regularSkills.Add(this.GetNodeOrNull<BossSkill>("CodeNuke"));

        // Clears your deployment bar for a few seconds, but increases deployment limit after
        this.regularSkills.Add(this.GetNodeOrNull<BossSkill>("QueueFree"));

        // Wipes all enemies / towers in a horizontal / vertical strip
        this.modeSkills.Add(this.GetNodeOrNull<BossSkill>("NullError"));

        // Turns one of your units hostile
        this.modeSkills.Add(this.GetNodeOrNull<BossSkill>("CosmicBitFlip"));

        // Unending machine gun attack until you "break" the "loop" (shield)
        this.modeSkills.Add(this.GetNodeOrNull<BossSkill>("WhileLoop"));

        // Hot potato with units. You must "return" the bomb by relocating to her
        this.modeSkills.Add(this.GetNodeOrNull<BossSkill>("RecursiveCall"));

        // Simon says, or in reverse? Read the if else statement
        this.modeSkills.Add(this.GetNodeOrNull<BossSkill>("IfElse"));

        // Switches mode and greatly reduces tower damage, use her skills against her
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("ComputationMode"));

        // When computational mode health reaches zero, is incapacitated for some time
        // this.skills.Add(this.GetNodeOrNull<BossSkill>("ExitComputation"));
    }

    public virtual void Initialize()
    {
        this.pathing = new BossPathing(this);
    }

    protected BossSkill BasicChooseSkill()
    {
        List<BossSkill> usable = this.regularSkills.Where(skill => skill.IsUsable()).ToList();
        usable.Sort((e1, e2) => e1.GetPriority().CompareTo(e2.GetPriority()));
        return usable.LastOrDefault();
    }

    protected BossSkill ModeChooseSkill()
    {
        List<BossSkill> usable = this.modeSkills.Where(skill => skill.IsUsable()).ToList();
        usable.Sort((e1, e2) => e1.GetPriority().CompareTo(e2.GetPriority()));
        return usable.LastOrDefault();
    }

}