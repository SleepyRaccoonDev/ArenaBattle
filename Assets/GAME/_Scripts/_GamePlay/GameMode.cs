using System;
using UnityEngine;

public class GameMode : IDisposable
{
    public event Action IsWined;
    public event Action IsDefeated;

    private IGameCondition _winCondition;
    private IGameCondition _defeatCondition;

    private ReactiveList<Character> _reactiveList;

    private EnemySpawner _enemySpawner;

    public GameMode(
        IGameCondition winCondition,
        IGameCondition defeatCondition,
        ReactiveList<Character> reactiveList,
        EnemySpawner enemySpawner)
    {
        _winCondition = winCondition;
        _defeatCondition = defeatCondition;

        _reactiveList = reactiveList;

        _enemySpawner = enemySpawner;
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
        EndGameProcess();
    }

    private void Loose()
    {
        IsDefeated?.Invoke();
        EndGameProcess();
    }

    private void EndGameProcess()
    {
        if (_reactiveList != null)
            foreach (var enemy in _reactiveList.List)
                GameObject.Destroy(enemy.gameObject);

        _enemySpawner?.StopSpawnProcces();

        _reactiveList.Clear();

        Dispose();
    }
}