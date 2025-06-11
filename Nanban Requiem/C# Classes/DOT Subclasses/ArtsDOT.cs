using System;
using Godot;

public partial class ArtsDOT : DamageOverTime
{
    public override void _Ready()
    {
        this.attack = new ArtsAttack();
    }
}