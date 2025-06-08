using System;

// Main interface for all units that can be targeted and hit
public interface IUnit
{

    public event EventHandler Despawning;

    public bool CanTarget();

    public void TakePhysicalDamage(int damage);

    public void TakeArtsDamage(int damage);

    public void Despawn();

}