using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyID enemyID;
    [Header("Shared")] public Stat maxHealth;
    public Stat attackPower;
    public Stat magicPower;
    public Stat armor;
    public Stat magicResistance;

    [Header("Warrior")] public Stat agility;
    public Stat intelligence;
    public Stat strength;
    public Stat vitality;
    public Stat criticalPower;
    public Stat criticalChance;

    [Header("Mage")] public Stat evasion;
    public Stat lighting;
    public Stat chill;
    public Stat ignite;

#if UNITY_EDITOR
    protected void OnValidate()
    {
        if (enemyID == null) return;

        name = enemyID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        if (string.IsNullOrEmpty(assetPath)) return;

        EditorApplication.delayCall += () => { AssetDatabase.RenameAsset(assetPath, name); };
    }
#endif
}