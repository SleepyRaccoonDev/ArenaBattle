using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharactersFactory : IDisposable
{
    private List<Controller> _controllers = new List<Controller>();

    public IReadOnlyList<Controller> Controllers => _controllers;

    private ReactiveList<Character> _reactiveList;

    public Character CreateEnemy(
        ReactiveList<Character> reactiveList,
        ControllersFactory controllersFactory,
        EnemyConfig enemyConfig,
        TouchConfig touchConfig,
        Vector3 spawnPoint)
    {
        _reactiveList = reactiveList;

        var enemyPrefub = enemyConfig.CharacterPrefabs[Random.Range(0, enemyConfig.CharacterPrefabs.Length)];

        Character enemy = GameObject.Instantiate(enemyPrefub, spawnPoint, Quaternion.identity, null);

        InitializeCharacter(enemy);

        var controller = controllersFactory.CreateRandomMoveController(enemy, enemy, enemyConfig, touchConfig);

        _controllers.Add(controller);

        controller.Start();

        reactiveList.Add(enemy);
        enemy.IsKilled += _reactiveList.Remove;

        return enemy;
    }

    public Character CreatPlayer(
        ControllersFactory controllersFactory,
        PlayerConfig playerConfig,
        BulletConfig bulletConfig,
        CommonConfig commonConfig)
    {
        var inputSystem = new InputSystem(Camera.main, commonConfig.GroundLayer);

        Character character = GameObject.Instantiate(
            playerConfig.CharacterPrefab,
            commonConfig.MainHeroStartPosition,
            Quaternion.identity,
            null);

        InitializeCharacter(character);

        var controller = controllersFactory.CreateWASDController(character, inputSystem, character, playerConfig, bulletConfig);

        _controllers.Add(controller);

        controller.Start();

        return character;
    }

    public void Dispose()
    {
        if (_reactiveList != null)
            foreach(Character enemy in _reactiveList.List)
                enemy.IsKilled -= _reactiveList.Remove;
    }

    private void InitializeCharacter(Character character)
    {
        Rigidbody rigidbody = character.GetComponent<Rigidbody>();
        rigidbody.useGravity = false;

        Mover mover = new Mover(rigidbody);
        Rotator rotator = new Rotator(rigidbody);
        Dasher dasher = new Dasher(rigidbody, character);

        GroundChecker groundChecker = character.GetComponentInChildren<GroundChecker>();
        SkinnedMeshRenderer skinned = character.GetComponentInChildren<SkinnedMeshRenderer>();

        character.Initialize(rigidbody, mover, rotator, dasher, groundChecker, skinned);
    }
}