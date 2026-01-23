using System;

public sealed class KillNEnemiesCondition : IGameCondition
{
    public event Action Triggered;

    private readonly ReactiveList<Character> _enemies;
    private readonly int _needKills;
    private int _killed;

    public KillNEnemiesCondition(ReactiveList<Character> enemies, int needKills)
    {
        _enemies = enemies;
        _needKills = needKills;
    }

    public void Activate()
    {
        _killed = 0;
        _enemies.IsRemoved += OnEnemyRemoved;
    }

    private void OnEnemyRemoved()
    {
        _killed++;

        if (_killed >= _needKills)
            Triggered?.Invoke();
    }

    public void Dispose() => _enemies.IsRemoved -= OnEnemyRemoved;
}