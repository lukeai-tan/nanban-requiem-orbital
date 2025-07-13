using System;
using System.Collections.Generic;
using Godot;

public partial class Pharos : BossSkill
{
    
    protected Prts boss;
    [Export] protected double cooldown;
    protected double timeSinceLastUse = 0;
    protected Random rng = new Random();
    
    protected List<Vector2> positions1 = new()
    {
        new Vector2(0, 0),
        new Vector2(-64, 64),
        new Vector2(-64, -64),
        new Vector2(64, -64),
        new Vector2(64, 64),
        new Vector2(-128, 128),
        new Vector2(-128, -128),
        new Vector2(128, -128),
        new Vector2(128, 128),
    };
    
    protected List<Vector2> positions2 = new()
    {
        new Vector2(0, 0),
        new Vector2(0, 64),
        new Vector2(0, -64),
        new Vector2(-64, 0),
        new Vector2(64, 0),
        new Vector2(0, 128),
        new Vector2(0, -128),
        new Vector2(-128, 0),
        new Vector2(128, 0),
    }; 

    protected List<Vector2> positions3 = new()
    {
        new Vector2(0, 0),
        new Vector2(-64, 64),
        new Vector2(-64, -64),
        new Vector2(64, -64),
        new Vector2(64, 64),
        new Vector2(0, 64),
        new Vector2(0, -64),
        new Vector2(-64, 0),
        new Vector2(64, 0),
    };

    protected List<Vector2> positions4 = new()
    {
        new Vector2(0, 0),
        new Vector2(-64, 64),
        new Vector2(-64, -64),
        new Vector2(64, -64),
        new Vector2(64, 64),
        new Vector2(0, 128),
        new Vector2(0, -128),
        new Vector2(-128, 0),
        new Vector2(128, 0),
    };

    public override void _Ready()
    {
        this.priority = 3;
        this.boss = this.GetParentOrNull<Prts>();
        this.boss.HasTower += (object boss, BoolEventArgs e) => this.UseCheck(e.boolean);
        this.boss.Half += (object boss, EventArgs e) => this.cooldown = 20;
    }
    
    public override void _Process(double delta)
    {
        this.timeSinceLastUse += delta;
    }

    public void UseCheck(bool hasTarget)
    {
        if (hasTarget && this.timeSinceLastUse >= this.cooldown)
        {
            this.usable = true;
        }
        else
        {
            this.usable = false;
        }
    }

    public override void Execute()
    {
        int number = this.rng.Next(4);
        switch (number)
        {
            case 0:
                this.boss.Pharos(this.positions1);
                break;
            case 1:
                this.boss.Pharos(this.positions2);
                break;
            case 2:
                this.boss.Pharos(this.positions3);
                break;
            case 3:
                this.boss.Pharos(this.positions4);
                break;
        }
        this.timeSinceLastUse = 0;
    }

}