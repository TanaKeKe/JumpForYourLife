using System;
using System.Collections;
using System.Collections.Generic;
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

    public AudioClip BackgroundSound { get { return backgroundSound; } }
    public AudioClip BreakSound { get { return breakSound; } }
    public AudioClip JumpSound { get { return jumpSound; } }
    public AudioClip GameOverSound { get { return gameOverSound; } }
    private void Start()
    {
        _saveTimeMusicPause = 0f;
        musicSource.clip = backgroundSound;
        LoadDataMusic();
        LoadDataSound();
    }

    private void LoadDataSound()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            soundSource.volume = PlayerPrefs.GetFloat("soundVolume");
        }
        else
        {
            soundSource.volume = 1f;
        }

        if (PlayerPrefs.HasKey("soundState"))
        {
            if (PlayerPrefs.GetString("soundState").Equals("ON"))
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
            PlayerPrefs.SetString("soundState", "ON");
            ContinueSound();
        }

    }

    private void LoadDataMusic()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSource.volume = 1f;
        }

        if (PlayerPrefs.HasKey("musicState"))
        {
            if (PlayerPrefs.GetString("musicState").Equals("ON"))
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
            PlayerPrefs.SetString("musicState", "ON");
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