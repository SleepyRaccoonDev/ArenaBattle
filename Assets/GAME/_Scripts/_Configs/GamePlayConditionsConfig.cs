using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "GamePlayConditionsConfig", menuName = "Configs/GamePlayConditionsConfig")]
public class GamePlayConditionsConfig : ScriptableObject
{
    [field: SerializeField] public WinConditions WinConditions { get; private set; }
    [field: SerializeField] public DefeatConditions DefeatConditions { get; private set; }

    [field: SerializeField, Min(0)] public float TimeForSurvive { get; private set; }
    [field: SerializeField, Min(0)] public int NumberOfEnemiesForKill { get; private set; }
    [field: SerializeField, Min(0)] public int CountOfEnemiesSpawnedForDefeat { get; private set; }

}