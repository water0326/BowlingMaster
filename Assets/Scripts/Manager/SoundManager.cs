using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    instance = go.AddComponent<SoundManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    [SerializeField] private AudioSource bgmSource;
    private AudioMixer audioMixer;
    private Queue<AudioSource> sfxPool;
    private List<AudioSource> activeSfx;
    private const int POOL_SIZE = 10;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeSoundSystem();
    }

    private void InitializeSoundSystem()
    {
        audioMixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
        
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgmSource.loop = true;

        sfxPool = new Queue<AudioSource>();
        activeSfx = new List<AudioSource>();

        for (int i = 0; i < POOL_SIZE; i++)
        {
            CreateSfxSource();
        }
    }

    private void CreateSfxSource()
    {
        AudioSource sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];
        sfxSource.playOnAwake = false;
        sfxPool.Enqueue(sfxSource);
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return;
        
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource sfxSource = GetSfxSource();
        sfxSource.clip = clip;
        sfxSource.Play();
        activeSfx.Add(sfxSource);
        StartCoroutine(ReturnToPool(sfxSource, clip.length));
    }

    private AudioSource GetSfxSource()
    {
        if (sfxPool.Count == 0)
        {
            CreateSfxSource();
        }
        return sfxPool.Dequeue();
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (activeSfx.Contains(source))
        {
            activeSfx.Remove(source);
            sfxPool.Enqueue(source);
        }
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }

    public void PlayBGMFromPath(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogError($"BGM 클립을 찾을 수 없습니다: {path}");
            return;
        }
        PlayBGM(clip);
    }

    public void PlaySFXFromPath(string path)
    {
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            Debug.LogError($"SFX 클립을 찾을 수 없습니다: {path}");
            return;
        }
        PlaySFX(clip);
    }
}
