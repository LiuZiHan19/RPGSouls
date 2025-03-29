using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDataManifest", menuName = "Scriptable Object/Audio/AudioDataManifest")]
public class AudioDataManifest : ScriptableObject
{
    public List<AudioData> audioDatas;
}