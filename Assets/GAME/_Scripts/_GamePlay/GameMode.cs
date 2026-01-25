using System;

public class GameMode : IDisposable
{
    public event Action IsWined;
    public event Action IsDefeated;

    private IGameCondition _winCondition;
    private IGameCondition _defeatCondition;

    public GameMode(
        IGameCondition winCondition,
        IGameCondition defeatCondition)
    {
        _winCondition = winCondition;
        _defeatCondition = defeatCondition;
    }

    public void Start()
    {
        _winCondition.Triggered += Win;
        _defeatCondition.Triggered += Loose;

        _winCondition.Activate();
        _defeatCondition.Activate();
    }


    public void Dispose()
    {
        _winCondition.Triggered -= Win;
        _defeatCondition.Triggered -= Loose;

        _winCondition.Dispose();
        _defeatCondition.Dispose();
    }

    private void Win()
    {
        IsWined?.Invoke();
        Dispose();
    }

    private void Loose()
    {
        IsDefeated?.Invoke();
        Dispose();
    }
}