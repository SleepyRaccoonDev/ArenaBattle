using UnityEngine;

public class ControllersFactory
{
    public RandomMoveController CreateRandomMoveController(
        MonoBehaviour mono,
        Character character,
        EnemyConfig enemyConfig,
        TouchConfig touchConfig)
    {
        if (character.TryGetComponent<TouchDamager>(out TouchDamager touchDamager))
            touchDamager.Initialize(touchConfig);

        return new RandomMoveController(mono, character, character, character, character, enemyConfig);
    }

    public WASDController CreateWASDController(
        MonoBehaviour mono,
        InputSystem inputSystem,
        Character character,
        PlayerConfig playerConfig,
        BulletConfig bulletConfig
        )
    {
        Shooter shooter = character.GetComponentInChildren<Shooter>();

        return new WASDController(mono, inputSystem, character, character, character, character, playerConfig, bulletConfig, shooter);
    }
}