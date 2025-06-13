using System;
using System.Collections.Generic;
using Godot;

public class VampiricBlast
{

    protected PackedScene projectileScene;
    protected PackedScene aoeScene;
    protected PackedScene dotScene;
    protected Tower initiator;

    public VampiricBlast(PackedScene projectileScene, PackedScene aoeScene, PackedScene dotScene, Tower initiator)
    {
        this.projectileScene = projectileScene;
        this.aoeScene = aoeScene;
        this.dotScene = dotScene;
        this.initiator = initiator;
    }

    public List<Action> Skill(int damage, double multiplier)
    {
        AOERangedAttack aoe = new AOERangedAttack(this.projectileScene, this.initiator, this.aoeScene);
        aoe.SetAttackAndSpeed(new ArtsAttack(), 300);
        aoe.SetModifiers(damage, multiplier);
        BasicMeleeDOT dot = new BasicMeleeDOT(this.dotScene);
        dot.SetAttack(new ArtsAttack());
        BasicMeleeAttack heal = new BasicMeleeAttack();
        heal.SetAttack(new Heal());
        heal.SetModifiers(damage, multiplier);
        return new List<Action> { aoe, dot, heal };
    }

}