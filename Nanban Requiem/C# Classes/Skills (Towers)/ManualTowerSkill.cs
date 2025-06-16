using System;
using Godot;

public abstract partial class ManualTowerSkill : TowerSkill
{

    protected TextureButton button;
    protected bool ready = false;

    public override void _Ready()
    {
        base._Ready();
        this.button = this.GetNodeOrNull<TextureButton>("Button");
        if (this.button != null)
        {
            this.button.Connect("pressed", new Callable(this, nameof(Call)));
        }
    }

    public override void _Process(double delta)
    {
        this.points += delta;
        if (!this.ready && this.points >= this.cost)
        {
            this.IsReady();
        }
    }

    protected void IsReady()
    {
        if (this.button != null)
        {
            this.button.Visible = true;
            this.button.Disabled = false;
        }
    }

    protected override void ResetPoints()
    {
        base.ResetPoints();
        this.ready = false;
        if (this.button != null)
        {
            this.button.Visible = false;
            this.button.Disabled = true;
        }
    }

}