using Godot;

public partial class TowerUI : Control
{
    private Tower selectedTower;

    public override void _Ready()
    {
        var retreatButton = GetNode<TextureButton>("Retreat Button");
        retreatButton.Pressed += OnRetreatPressed;
        this.Visible = false;
    }

    public void ShowTowerUI(Tower tower)
    {
        selectedTower = tower;
        this.Visible = true;
        var towerName = tower.Name;
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
    }

    public void HideTowerUI()
    {
        selectedTower = null;
        this.Visible = false;
    }

    private void OnRetreatPressed()
    {
        if (selectedTower != null)
        {
            selectedTower.Retreat();
            this.Visible = false;
        }
    }
}
