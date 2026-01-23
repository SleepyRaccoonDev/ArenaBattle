using System;

public interface IGameCondition : IDisposable
{
    event Action Triggered;

    void Activate();
}