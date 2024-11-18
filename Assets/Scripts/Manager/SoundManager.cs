using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private AudioMixer audioMixer;

    private void Start()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
    }
}
