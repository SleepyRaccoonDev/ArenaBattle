using System;

public sealed class SpawnMoreThanNEnemiesCondition : IGameCondition
{
    public event Action Triggered;

    private readonly ReactiveList<Character> _enemies;
    private readonly int _limit;

    public SpawnMoreThanNEnemiesCondition(ReactiveList<Character> enemies,  int limit)
    {
        _enemies = enemies;
        _limit = limit;
    }

    public void Activate() => _enemies.IsAdded += OnAdded;

    private void OnAdded(int count)
    {
        if (count >= _limit)
            Triggered?.Invoke();
    }

    public void Dispose() => _enemies.IsAdded -= OnAdded;
}