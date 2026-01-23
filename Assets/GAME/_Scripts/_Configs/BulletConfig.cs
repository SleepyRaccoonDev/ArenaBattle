using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BulletConfig", menuName = "Configs/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }

    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public float Radius { get; private set; } = 4;
    [field: SerializeField] public float Damage { get; private set; } = 30;
}