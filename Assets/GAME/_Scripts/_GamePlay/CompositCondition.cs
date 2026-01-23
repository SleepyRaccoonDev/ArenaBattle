using System;

public class CompositCondition : IGameCondition
{
    public event Action Triggered;

    private IGameCondition[] _gameConditions;

    public CompositCondition(params IGameCondition[] gameConditions)
    {
        _gameConditions = gameConditions;
    }

    public void Activate()
    {
        foreach (var condition in _gameConditions)
        {
            condition.Triggered += OnTriggered;
            condition.Activate();
        }   
    }

    public void Dispose()
    {
        foreach (var condition in _gameConditions)
        {
            condition.Triggered -= OnTriggered;
            condition.Dispose();
        }
    }

    private void OnTriggered() => Triggered?.Invoke();
}