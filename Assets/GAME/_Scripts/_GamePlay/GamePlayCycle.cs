using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class GamePlayCycle : IDisposable
{
    private readonly KeyCode FKeyCod = KeyCode.F;

    private MonoBehaviour _context;

    private Character _player;

    private PlayerSpawner _playerSpawner;
    private PlayerConfig _playerConfig;
    private CommonConfig _commonConfig;
    private BulletConfig _bulletConfig;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    private CinemachineVirtualCamera _cinemachine;

    private ConfirmPopup _confirmPopup;

    private GameMode _gameMode;
    private GamePlayConditionsConfig _gamePlayConditionsConfig;
    private ReactiveList<Character> _enemyesList;
    private EnemySpawner _enemySpawner;

    public GamePlayCycle(
        MonoBehaviour context,
        PlayerSpawner playerSpawner,
        PlayerConfig playerConfig,
        CommonConfig commonConfig,
        BulletConfig bulletConfig,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        CinemachineVirtualCamera cinemachine,
        ConfirmPopup confirmPopup,
        GamePlayConditionsConfig gamePlayConditionsConfig,
        ReactiveList<Character> reactiveList,
        EnemySpawner enemySpawner)
    {
        _context = context;
        _playerSpawner = playerSpawner;
        _playerConfig = playerConfig;
        _commonConfig = commonConfig;
        _bulletConfig = bulletConfig;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _cinemachine = cinemachine;
        _confirmPopup = confirmPopup;
        _gamePlayConditionsConfig = gamePlayConditionsConfig;
        _enemyesList = reactiveList;
        _enemySpawner = enemySpawner;
    }

    public void Prepare()
    {   
        _player = _playerSpawner.Spawn(
                  _playerConfig,
                  _commonConfig,
                  _bulletConfig,
                  _controllersFactory,
                  _charactersFactory,
                  _cinemachine);
    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.SetText($"Нажмите {FKeyCod.ToString()} для продолжения.");
        _enemySpawner.StopSpawnProcces();

        yield return _confirmPopup.WaitConfirm(FKeyCod);

        _confirmPopup.Hide();

        if (_player == null)
            Prepare();

        GamePlayConditionsFactory gamePlayConditionsController = new GamePlayConditionsFactory(
            _gamePlayConditionsConfig,
            _context,
            _enemyesList,
            _player);

        _gameMode = new GameMode(
            gamePlayConditionsController.CreateWinCondition(),
            gamePlayConditionsController.CreateDefeatCondition());

        _enemySpawner.SpawnOnStart();

        _enemySpawner.StartSpawnProcces();

        _gameMode.IsWined += OnGameModeIsWined;
        _gameMode.IsDefeated += OnGameModeIsDefeated;

        _gameMode.Start();
    }

    public void Dispose()
    {
        if (_gameMode != null)
        {
            _gameMode.IsWined += OnGameModeIsWined;
            _gameMode.IsDefeated += OnGameModeIsDefeated;
        }
    }

    private void OnGameModeIsDefeated()
    {
        Debug.Log("Defeat");
        Prepare();
        EndGameProcess();
    }

    private void OnGameModeIsWined()
    {
        Debug.Log("Win");
        EndGameProcess();
    }

    private void EndGameProcess()
    {
        if (_enemyesList != null)
            foreach (var enemy in _enemyesList.List)
                GameObject.Destroy(enemy.gameObject);

        _enemySpawner?.StopSpawnProcces();

        _enemyesList.Clear();

        Dispose();
        _context.StartCoroutine(Launch());
    }
}