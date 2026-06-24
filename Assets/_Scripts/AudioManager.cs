using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("BGM")]
    public AudioClip bgmClip;
    [Range(0f, 1f)] public float bgmVolume = 0.4f;

    [Header("SFX")]
    public AudioClip spinClip;
    public AudioClip winClip;
    public AudioClip noWinClip;
    public AudioClip buttonClickClip;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // BGM source — loops forever
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.clip   = bgmClip;
        bgmSource.loop   = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = false;

        // SFX source — plays one shot at a time
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop   = false;
        sfxSource.volume = sfxVolume;
        sfxSource.playOnAwake = false;
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (bgmClip == null) return;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySpin()    => PlaySFX(spinClip);
    public void PlayWin()     => PlaySFX(winClip);
    public void PlayNoWin()   => PlaySFX(noWinClip);
    public void PlayClick()   => PlaySFX(buttonClickClip);

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}