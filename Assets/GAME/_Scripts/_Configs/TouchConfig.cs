using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TouchConfig", menuName = "Configs/TouchConfig")]
public class TouchConfig : ScriptableObject
{
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public float Damage { get; private set; } = 30;
}