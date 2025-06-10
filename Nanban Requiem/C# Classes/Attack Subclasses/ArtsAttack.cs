using System;

public class ArtsAttack : Attack
{

    public override void Hit(IUnit target, int damage, double multiplier)
    {
        int finalDamage = (int) Math.Round(damage * multiplier);
        target.TakeArtsDamage(finalDamage);
    }

    public override string ToString()
    {
        return "Arts Attack";
    }

}
