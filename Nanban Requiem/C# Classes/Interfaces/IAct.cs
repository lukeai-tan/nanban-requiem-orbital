using System;

// An interface for units to define which action to take at a particular moment
public interface IAct
{

    public event EventHandler TakeAction;

    public void SetActions();

    public void Act();

}