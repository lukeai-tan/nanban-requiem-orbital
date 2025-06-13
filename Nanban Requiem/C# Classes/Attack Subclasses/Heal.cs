using System;

public class Heal : Attack
{

    public override void Hit(Unit target, int heal, double multiplier)
    {
        int finalHealing = (int) Math.Round(heal * multiplier);
        target.Heal(finalHealing);
    }

    public override string ToString()
    {
        return "Heal";
    }

}
