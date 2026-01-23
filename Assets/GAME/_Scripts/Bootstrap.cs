using System.Collections;
using UnityEngine;
using Cinemachine;

public class Bootstrap : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private ScreenLoader _screenLoader;
    [SerializeField] private ConfirmPopup _confirmPopup;

    [Space(10)]
    [SerializeField] private CinemachineVirtualCamera _cinemachine;

    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    private EnemySpawner _enemySpawner;

    private GamePlayCycle _gamePlayCycle;

    private void Awake()
    {
        StartCoroutine(StartProcces());
    }

    private IEnumerator StartProcces()
    {
        _screenLoader.Show();

        _screenLoader.ShowMessage("Loading...");

        GamePlayConditionsConfig gamePlayConditionsConfig = Resources.Load<GamePlayConditionsConfig>("Configs/GamePlayConditionsConfig");
        EnemyConfig enemyConfig = Resources.Load<EnemyConfig>("Configs/EnemyConfig");
        PlayerConfig playerConfig = Resources.Load<PlayerConfig>("Configs/PlayerConfig");
        BulletConfig bulletConfig = Resources.Load<BulletConfig>("Configs/BulletConfig");
        CommonConfig commonConfig = Resources.Load<CommonConfig>("Configs/CommonConfig");
        TouchConfig touchConfig = Resources.Load<TouchConfig>("Configs/TouchConfig");

        _controllersFactory = new ControllersFactory();
        _charactersFactory = new CharactersFactory();

        ReactiveList<Character> reactiveList = new ReactiveList<Character>();

        _enemySpawner = new EnemySpawner(
            this,
            reactiveList,
            enemyConfig,
            touchConfig,
            _controllersFactory,
            _charactersFactory,
            commonConfig);

        PlayerSpawner playerSpawner = new PlayerSpawner();

        _gamePlayCycle = new GamePlayCycle(
            this,
            playerSpawner,
            playerConfig,
            commonConfig,
            bulletConfig,
            _controllersFactory,
            _charactersFactory,
            _cinemachine,
            _confirmPopup,
            gamePlayConditionsConfig,
            reactiveList,
            _enemySpawner
            );

        yield return new WaitForSeconds(1.5f);

        _gamePlayCycle.Prepare();

        _screenLoader.Hide();

        yield return _gamePlayCycle.Launch();
    }

    private void OnDisable()
    {
        if (_charactersFactory != null)
            foreach (var controller in _charactersFactory.Controllers)
                controller.Dispose();

        _enemySpawner?.Dispose();

        _charactersFactory?.Dispose();

        _gamePlayCycle?.Dispose();
    }
}