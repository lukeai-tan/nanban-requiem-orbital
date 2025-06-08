using System;

// An interface implemented by enemies that can be blocked
public interface IBlock
{

    public int GetBlockCount();

    public bool IsBlocked();

    public void Blocked(Tower blocker);

    public void Unblocked(object blocker, EventArgs e);

}