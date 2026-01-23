using System;

public sealed class PlayerDiedCondition : IGameCondition
{
    public event Action Triggered;

    private readonly Character _player;

    public PlayerDiedCondition(Character player) => _player = player;

    public void Activate()
    {
        if (_player != null)
            _player.IsKilled += OnKilled;
    }

    private void OnKilled(Character c) => Triggered?.Invoke();

    public void Dispose()
    {
        if (_player != null)
            _player.IsKilled -= OnKilled;
    }
}