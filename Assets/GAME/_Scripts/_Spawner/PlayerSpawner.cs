using Cinemachine;

public class PlayerSpawner
{
    public Character Spawn(
        PlayerConfig playerConfig,
        CommonConfig commonConfig,
        BulletConfig bulletConfig,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        CinemachineVirtualCamera cinemachineVirtual)
    {
        Character character = charactersFactory.CreatPlayer(controllersFactory, playerConfig, bulletConfig, commonConfig);

        cinemachineVirtual.Follow = character.transform;
        cinemachineVirtual.LookAt = character.transform;

        return character;
    }
}