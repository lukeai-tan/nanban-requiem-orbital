using Godot;
using System;

public partial class UI : CanvasLayer
{
    private TextureProgressBar baseHpBar;
    public TowerBuilder towerBuilder;

    public override void _Ready()
    {
        baseHpBar = GetNode<TextureProgressBar>("HUD/InfoBar/HBoxContainer/BaseHPBar");
    }

    public Control SetTowerPreview(string towerType, Vector2 mousePosition)
    {
        var baseSprite = new Sprite2D
        {
            Texture = GD.Load<Texture2D>($"res://Assets/Towers/{towerType}_base.png"),
            Name = "BaseSprite"
        };

        var rangeTexture = new Sprite2D
        {
            Texture = GD.Load<Texture2D>("res://Assets/Towers/range_overlay.png"),
            Name = "RangeOverlay",
            Position = Vector2.Zero
        };
        var gameData = (Node)GetNode("/root/GameData");
        var towerData = (Godot.Collections.Dictionary)gameData.Get("tower_data");
        var towerDict = (Godot.Collections.Dictionary)towerData[towerType];
        double rangeDouble = (double)towerDict["range"];
        float scaling = (float)rangeDouble / 600.0f;

        rangeTexture.Scale = new Vector2(scaling, scaling);

        var control = new Control
        {
            Name = "TowerPreview",
            Position = mousePosition
        };

        control.AddChild(rangeTexture);
        control.AddChild(baseSprite);

        if (towerType.StartsWith("RangedTower"))
        {
            string texturePath = $"res://Assets/Towers/{towerType}_turret.png";

            if (FileAccess.FileExists(texturePath))
            {
                var turretSprite = new Sprite2D
                {
                    Texture = GD.Load<Texture2D>(texturePath),
                    Name = "TurretSprite"
                };
                control.AddChild(turretSprite);
            }
        }

        AddChild(control);
        MoveChild(control, 0);
        return control;
    }

    public void UpdateTowerPreview(Vector2 newPosition, string color)
    {
        var preview = GetNode<Control>("TowerPreview");
        preview.Position = newPosition;

        var colorValue = new Color(color);

        if (preview.HasNode("BaseSprite"))
        {
            var baseSprite = preview.GetNode<Sprite2D>("BaseSprite");
            baseSprite.TextureFilter = CanvasItem.TextureFilterEnum.Nearest;

            if (baseSprite.Modulate != colorValue)
                baseSprite.Modulate = colorValue;
        }

        if (preview.HasNode("TurretSprite"))
        {
            var turretSprite = preview.GetNode<Sprite2D>("TurretSprite");
            turretSprite.Modulate = colorValue;
        }

        if (preview.HasNode("RangeOverlay"))
        {
            var rangeSprite = preview.GetNode<Sprite2D>("RangeOverlay");
            rangeSprite.Modulate = colorValue;
        }
    }


    private void OnPausePlayPressed()
    {
        if (towerBuilder.BuildMode)
            towerBuilder.CancelBuildMode();

        GetTree().Paused = !GetTree().Paused;
    }

    private void OnFastForwardPressed()
    {
        if (towerBuilder.BuildMode)
            towerBuilder.CancelBuildMode();

        if (Engine.TimeScale == 2.0f)
            Engine.TimeScale = 1.0f;
        else
            Engine.TimeScale = 2.0f;
    }

    private void OnRestartPressed()
    {
        GetTree().ReloadCurrentScene();
    }

    public void UpdateHealthBar(float baseHealth)
    {
        var hpBarTween = baseHpBar.CreateTween();
        hpBarTween.TweenProperty(baseHpBar, "value", baseHealth, 0.1);

        if (baseHealth >= 5.0f)
            baseHpBar.TintProgress = new Color("3cc510"); // Green
        else if (baseHealth <= 4.0f && baseHealth >= 2.0f)
            baseHpBar.TintProgress = new Color("e1be32"); // Orange
        else
            baseHpBar.TintProgress = new Color("e11e1e"); // Red
    }
}
