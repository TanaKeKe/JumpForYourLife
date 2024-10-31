using Common;
using UnityEngine;

public class AudioGamePlayManager : Singleton<AudioGamePlayManager>
{
    [Header("----------Audio Source----------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("----------Audio Clip----------")]
    [SerializeField] private AudioClip backgroundSound;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip gameOverSound;

    private float _saveTimeMusicPause;

    public AudioClip BackgroundSound =>  backgroundSound; 
    public AudioClip BreakSound =>  breakSound; 
    public AudioClip JumpSound =>  jumpSound; 
    public AudioClip GameOverSound =>  gameOverSound; 
    

    private void Start()
    {
        _saveTimeMusicPause = 0f;
        musicSource.clip = backgroundSound;
        LoadDataMusic();
        LoadDataSound();
    }

    private void LoadDataSound()
    {
        if (PlayerPrefs.HasKey(GamePrefs.SOUND_VOLUME_KEY))
        {
            soundSource.volume = PlayerPrefs.GetFloat(GamePrefs.SOUND_VOLUME_KEY);
        }
        else
        {
            soundSource.volume = 1f;
        }

        if (PlayerPrefs.HasKey(GamePrefs.SOUND_KEY))
        {
            if (PlayerPrefs.GetInt(GamePrefs.SOUND_KEY) == 1)
            {
                ContinueSound();
            }
            else
            {
                StopSound();
            }
        }
        else
        {
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 1);
            ContinueSound();
        }
    }

    private void LoadDataMusic()
    {
        if (PlayerPrefs.HasKey(GamePrefs.MUSIC_VOLUME_KEY))
        {
            musicSource.volume = PlayerPrefs.GetFloat(GamePrefs.MUSIC_VOLUME_KEY);
        }
        else
        {
            musicSource.volume = 1f;
        }

        if (PlayerPrefs.HasKey(GamePrefs.MUSIC_KEY))
        {
            if (PlayerPrefs.GetInt(GamePrefs.MUSIC_KEY) == 1)
            {
                ContinueMusic();
            }
            else
            {
                StopMusic();
            }
        }
        else
        {
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 1);
            ContinueMusic();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        _saveTimeMusicPause = musicSource.time;
        musicSource.Pause();
    }

    public void ContinueMusic()
    {
        musicSource.time = _saveTimeMusicPause;
        musicSource.Play();
    }

    public void StopSound()
    {
        soundSource.mute = true;
    }

    public void ContinueSound()
    {
        soundSource.mute = false;
    }
}