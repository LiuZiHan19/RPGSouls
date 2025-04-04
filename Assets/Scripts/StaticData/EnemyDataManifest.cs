using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataManifest", menuName = "Scriptable Object/Enemy/EnemyDataManifest")]
public class EnemyDataManifest : ScriptableObject
{
    public List<EnemyData> enemyDataList;
}