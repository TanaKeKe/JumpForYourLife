using Common;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeHomeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey(GamePrefs.MUSIC_VOLUME_KEY) && PlayerPrefs.HasKey(GamePrefs.SOUND_VOLUME_KEY))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSoundVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myAudioMixer.SetFloat(GameConfig.MUSIC, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GamePrefs.MUSIC_VOLUME_KEY, volume);
    }

    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        myAudioMixer.SetFloat(GameConfig.SOUND, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(GamePrefs.SOUND_VOLUME_KEY, volume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat(GamePrefs.MUSIC_VOLUME_KEY);
        soundSlider.value = PlayerPrefs.GetFloat(GamePrefs.SOUND_VOLUME_KEY);

        SetMusicVolume();
        SetSoundVolume();
    }
}
