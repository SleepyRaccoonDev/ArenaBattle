using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "CommonConfig", menuName = "Configs/CommonConfig")]
public class CommonConfig : ScriptableObject
{
    [field: SerializeField] public LayerMask GroundLayer { get; private set; }

    [field: SerializeField] public Vector3 MainHeroStartPosition { get; private set; }

    [field: SerializeField] public List<Vector3> EnemyesStartPositions { get; private set; } = new List<Vector3>();

    [field: SerializeField] public float TimeForSpawnEnemy { get; private set; } = 5;

    [ContextMenu("SetMainHeroStartPosition")]
    private void SetMainHeroStartPosition()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("StartHeroPosition");

        MainHeroStartPosition = gameObject.transform.position;
    }

    [ContextMenu("SetEnemyesStartPositions")]
    private void SetEnemyesStartPositions()
    {
        EnemyesStartPositions.Clear();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("StartEnemyPosition");

        foreach (var position in gameObjects)
            EnemyesStartPositions.Add(position.transform.position);
    }
}