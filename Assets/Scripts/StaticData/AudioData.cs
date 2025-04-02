using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "Scriptable Object/Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioID AudioID;
    public AudioClip audioClip;

#if UNITY_EDITOR
    protected void OnValidate()
    {
        name = AudioID.ToString();
        string assetPath = AssetDatabase.GetAssetPath(this);
        EditorApplication.delayCall += () =>
        {
            AssetDatabase.RenameAsset(assetPath, name);
            AssetDatabase.SaveAssets();
        };
    }
#endif
}