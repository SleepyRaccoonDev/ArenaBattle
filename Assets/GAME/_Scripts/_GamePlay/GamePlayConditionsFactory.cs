using System;
using UnityEngine;

public class GamePlayConditionsFactory
{
    private GamePlayConditionsConfig _gamePlayConditionsConfig;

    private MonoBehaviour _mono;

    private ReactiveList<Character> _reactiveList;

    private Character _mainHero;

    public GamePlayConditionsFactory(
        GamePlayConditionsConfig gamePlayConditionsConfig,
        MonoBehaviour mono,
        ReactiveList<Character> reactiveList,
        Character MainHero)
    {
        _gamePlayConditionsConfig = gamePlayConditionsConfig;
        _mono = mono;
        _reactiveList = reactiveList;

        _mainHero = MainHero;
    }

    public IGameCondition CreateWinCondition()
    {
        switch (_gamePlayConditionsConfig.WinConditions)
        {
            case WinConditions.SurviveForNSecondsAndNotDie:
                return new SurviveForSecondsCondition(_mono, _gamePlayConditionsConfig.TimeForSurvive);

            case WinConditions.KillNNumberOfEnemies:
                return new KillNEnemiesCondition(_reactiveList, _gamePlayConditionsConfig.NumberOfEnemiesForKill);

            default:
                throw new Exception("InvalidWinCondition");
        }
    }

    public IGameCondition CreateDefeatCondition()
    {
        switch (_gamePlayConditionsConfig.DefeatConditions)
        {
            case DefeatConditions.MoreThanNEnemiesSpawned:
                return new CompositCondition(
                    new SpawnMoreThanNEnemiesCondition(_reactiveList, _gamePlayConditionsConfig.CountOfEnemiesSpawnedForDefeat),
                    new PlayerDiedCondition(_mainHero)
                    );

            case DefeatConditions.PlayerDied:
                return new PlayerDiedCondition(_mainHero);

            default:
                throw new Exception("InvalidDefeatCondition");
        }
    }
}