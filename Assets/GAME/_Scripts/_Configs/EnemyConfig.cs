using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public Character[] CharacterPrefabs { get; private set; }

    [field: SerializeField, Min(.5f)] public float MinTimeToChangeDirection { get; private set; } = 1.5f;
    [field: SerializeField, Min(1)] public float MaxTimeToChangeDirection { get; private set; } = 3f;

    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 9f;
    [field: SerializeField, Min(0)] public float RotateSpeed { get; private set; } = 400f;

    [field: SerializeField, Min(0)] public float DashPower { get; private set; } = 24;
    [field: SerializeField, Min(0)] public float DashTime { get; private set; } = 1.1f;
}