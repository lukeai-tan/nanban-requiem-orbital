using System;

public class BoolEventArgs : EventArgs
{

    public bool boolean;

    public BoolEventArgs(bool boolean)
    {
        this.boolean = boolean;
    }

}