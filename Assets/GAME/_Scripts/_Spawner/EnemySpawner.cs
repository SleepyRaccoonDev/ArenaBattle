using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : IDisposable
{
    private TimerService _timerService;

    private ReactiveList<Character> _reactiveList;
    private EnemyConfig _enemyConfig;
    private TouchConfig _touchConfig;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    private CommonConfig _commonConfig;

    public EnemySpawner(
        MonoBehaviour mono,
        ReactiveList<Character> reactiveList,
        EnemyConfig enemyConfig,
        TouchConfig touchConfig,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        CommonConfig commonConfig)
    {
        _timerService = new TimerService(mono);
        _reactiveList = reactiveList;
        _enemyConfig = enemyConfig;
        _touchConfig = touchConfig;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _commonConfig = commonConfig;

        _timerService.TimesUp += ChooseSpawnPoint;
    }

    public void Dispose() => _timerService.TimesUp -= ChooseSpawnPoint;

    public void SpawnOnStart()
    {
        foreach (Vector3 spawnPoint in _commonConfig.EnemyesStartPositions)
            Spawn(spawnPoint);
    }

    public void StartSpawnProcces() => Start();

    public void StopSpawnProcces() => _timerService.Pause();

    private void ChooseSpawnPoint()
    {
        int randomIndex = Random.Range(0, _commonConfig.EnemyesStartPositions.Count);
        Vector3 randomPosition = _commonConfig.EnemyesStartPositions[randomIndex];

        Spawn(randomPosition);

        Start();
    }

    private void Spawn(Vector3 spawnPoint)
    {
        _charactersFactory.CreateEnemy(_reactiveList, _controllersFactory, _enemyConfig, _touchConfig, spawnPoint);
    }

    private void Start() => _timerService.Start(_commonConfig.TimeForSpawnEnemy);
}