using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public class AudioItem
    {
        public string key;
        public AudioClip clip;
    }

    public List<AudioItem> bgmList = new List<AudioItem>();
    public List<AudioItem> sfxList = new List<AudioItem>();
} 