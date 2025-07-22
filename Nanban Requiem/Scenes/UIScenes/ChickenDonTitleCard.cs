using Godot;

public partial class ChickenDonTitleCard : Control
{
    private ChickenDon chickenDon;
    private TextureRect DagaKotowaru;
    private TextureRect YakuzaTitle;

    public override void _Ready()
    {
        DagaKotowaru = GetNode<TextureRect>("DagaKotowaru");
        YakuzaTitle = GetNode<TextureRect>("YakuzaTitle");
        DagaKotowaru.Visible = false;
        YakuzaTitle.Visible = false;
    }

    public void ShowTitleCard()
    {
        
    }
}