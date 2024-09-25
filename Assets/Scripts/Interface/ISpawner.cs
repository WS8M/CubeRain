using System;

public interface ISpawner
{
    public event Action Created;
    public event Action Spawned;

    public int GetCountActive();
}