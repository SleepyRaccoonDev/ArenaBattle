using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public Character CharacterPrefab { get; private set; }

    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 9f;
    [field: SerializeField, Min(0)] public float RotateSpeed { get; private set; } = 400f;

    [field: SerializeField, Min(0)] public float DashPower { get; private set; } = 20;
    [field: SerializeField, Min(0)] public float DashTime { get; private set; } = .7f;

    [field: SerializeField, Min(0)] public float ClampBackSpeed { get; private set; }
    [field: SerializeField, Min(0)] public float Degrees { get; private set; } = 0.87f;
}