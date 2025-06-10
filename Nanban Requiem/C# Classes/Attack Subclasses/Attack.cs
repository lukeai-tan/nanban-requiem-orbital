using System;
using System.Runtime;
using System.Text;

public abstract class Attack
{

    public abstract void Hit(IUnit target, int damage, double multiplier);

}
