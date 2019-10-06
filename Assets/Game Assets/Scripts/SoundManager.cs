using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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
                    instance = new GameObject("Spawned AudioManager", typeof(SoundManager)).GetComponent<SoundManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    private AudioSource musicSource;
    private AudioSource musicSource2;
    private AudioSource sfxSource;
    private AudioSource LaserSource;
    private bool IsFirstMusicPlaying;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
        LaserSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource2.loop = true;
        LaserSource.loop = true;
        sfxSource.outputAudioMixerGroup = GameManager.Instance.SoundEffectGroup;
        musicSource.outputAudioMixerGroup = GameManager.Instance.MusicGroup;
        musicSource2.outputAudioMixerGroup = GameManager.Instance.MusicGroup;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        AudioSource activeSource = (IsFirstMusicPlaying) ? musicSource : musicSource2;
        activeSource.clip = musicClip;
        activeSource.volume = 1;
        activeSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip clip, float volume)
    {
        sfxSource.PlayOneShot(clip, volume);
    }
    public void PlayLaserLoop(AudioClip clip)
    {
        LaserSource.clip = clip;
        LaserSource.volume = 1;
        LaserSource.Play();
    }
    public void StopLaserLoop()
    {
        LaserSource.Stop();
    }
    public void PlayMusicWithFade(AudioClip newClip, float transitionTime = 1.0f)
    {
        AudioSource activeSource = (IsFirstMusicPlaying) ? musicSource : musicSource2;
        StartCoroutine(UpdateMusicWithFade(activeSource, newClip, transitionTime));
    }
    private IEnumerator UpdateMusicWithFade(AudioSource activeSource, AudioClip newClip, float transitionTime)
    {
        if (!activeSource.isPlaying)
            activeSource.Play();
        float t = 0.0f;
        // Fade Out
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (1 - (t / transitionTime));
            yield return null;
        }

        activeSource.Stop();
        activeSource.clip = newClip;
        activeSource.Play();

        // Fade In
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            activeSource.volume = (t / transitionTime);
            yield return null;
        }
    }

    public void PlayMusicWithCrossFade(AudioClip musicClip, float transitionTime = 1.0f)
    {
        //Determine Which Source is active
        AudioSource activeSouce = (IsFirstMusicPlaying) ? musicSource : musicSource2;
        AudioSource newSource = (IsFirstMusicPlaying) ? musicSource2 : musicSource;

        IsFirstMusicPlaying = !IsFirstMusicPlaying;

        newSource.clip = musicClip;
        newSource.Play();
        StartCoroutine(UpdateMusicWithCrossFade(activeSouce, newSource, transitionTime));
    }
    private IEnumerator UpdateMusicWithCrossFade(AudioSource original, AudioSource newSource, float transitionTime)
    {
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            original.volume = 1 - (t / transitionTime);
            newSource.volume = t / transitionTime;
            yield return null;
        }
        original.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        musicSource2.volume = volume;
    }
    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}
