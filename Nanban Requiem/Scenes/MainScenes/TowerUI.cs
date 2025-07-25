using Godot;

public partial class TowerUI : Control
{
    private Tower selectedTower;

    public override void _Ready()
    {
        var retreatButton = GetNode<TextureButton>("Retreat Button");
        retreatButton.Pressed += OnRetreatPressed;
        var closeButton = GetNode<TextureButton>("Close Button");
        closeButton.Pressed += OnClosePressed;
        this.Visible = false;
    }

    public override void _Process(double delta)
    {
        // hide UI if tower selected is null or invalid
        if (selectedTower == null || !GodotObject.IsInstanceValid(selectedTower))
        {
            this.Visible = false;
        }
        // keep game slowmo when tower UI visible
        if (this.Visible)
        {
            Engine.TimeScale = 0.3f;
        }
    }

    public void ShowTowerUI(Tower tower)
    {
        Engine.TimeScale = 0.3f;
        selectedTower = tower;
        this.Visible = true;
        var towerName = tower.GetType().Name;
        GD.Print($"Showing UI for tower: {towerName}");
        var gameData = GetNode("/root/GameData");

        var towerData = gameData.Get("tower_data").As<Godot.Collections.Dictionary>();
        if (!towerData.ContainsKey(towerName))
        {
            GD.PrintErr($"No data found for tower: {towerName}");
            return;
        }

        var towerInfo = towerData[towerName].As<Godot.Collections.Dictionary>();
        if (towerInfo == null)
        {
            GD.PrintErr($"towerInfo dictionary for {towerName} is null.");
            return;
        }

        string iconPath = towerInfo["sprite_in_game"].AsString();
        if (!string.IsNullOrEmpty(iconPath))
        {
            var iconTexture = GD.Load<Texture2D>(iconPath);
            GetNode<TextureRect>("TowerIcon").Texture = iconTexture;
        }

        string towerLoreName = towerInfo["name"].AsString();
        if (!string.IsNullOrEmpty(towerLoreName))
        {
            GetNode<Label>("Tower Name").Text = towerLoreName;
        }
        else
        {
            GD.PrintErr($"Tower name for {towerName} is null or empty.");
        }
    }

    public void HideTowerUI()
    {
        selectedTower = null;
        this.Visible = false;
        var gameData = GetNode("/root/GameData");
        var timeScale = gameData.Get("time_scale").AsSingle();
        Engine.TimeScale = timeScale;
    }

    private void OnRetreatPressed()
    {
        var dpBar = GetNode("/root/SceneHandler/GameScene/UI/HUD/DPBar");
        if (selectedTower != null && dpBar != null)
        {
            selectedTower.Retreat();
            this.Visible = false;
            dpBar.Call("gain_dp", 2);
        }
    }

    private void OnClosePressed()
    {
        HideTowerUI();
    }
}
