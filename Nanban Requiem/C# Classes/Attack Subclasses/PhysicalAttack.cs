using System;

public class PhysicalAttack : Attack
{

    public override void Hit(Unit target, int damage, double multiplier)
    {
        int finalDamage = (int) Math.Round(damage * multiplier);
        target.TakePhysicalDamage(finalDamage);
    }
    public override string ToString()
    {
        return "Physical Attack";
    }

}
