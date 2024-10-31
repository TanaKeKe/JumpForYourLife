using Common;
using UnityEngine;

public class PauseGamePanel : Panel
{
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private GameObject soundOff;

    private void Start()
    {
        LoadDataMusic();
        LoadDataSound();
    }

    private void LoadDataSound()
    {
        if (PlayerPrefs.HasKey(GamePrefs.SOUND_KEY))
        {
            if (PlayerPrefs.GetInt(GamePrefs.SOUND_KEY) == 1)
            {
                soundOn.SetActive(true);
                soundOff.SetActive(false);
            }
            else
            {
                soundOn.SetActive(false);
                soundOff.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY,1);
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
    }

    private void LoadDataMusic()
    {
        if (PlayerPrefs.HasKey(GamePrefs.MUSIC_KEY))
        {
            if (PlayerPrefs.GetInt(GamePrefs.MUSIC_KEY) == 1)
            {
                musicOn.SetActive(true);
                musicOff.SetActive(false);
            }
            else
            {
                musicOn.SetActive(false);
                musicOff.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 1);
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
    }

    public void Resume()
    {
        GamePlayController.Instance.isPause = false;
        GamePlayController.Instance.isResume = true;
        PanelManager.Instance.ClosePanel("PauseGamePanel");
    }

    public void Replay()
    {
        Messenger.Broadcast(EventKey.Replay);
    }

    public void ReturnHome()
    {
        Time.timeScale = 1f;
        Messenger.Broadcast(EventKey.GoHome);
    }

    public void CheckSound()
    {
        if (soundOn.activeSelf)
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            AudioGamePlayManager.Instance.StopSound();
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 0);
        }
        else
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            AudioGamePlayManager.Instance.ContinueSound();
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 1);
        }
    }

    public void CheckMusic()
    {
        if(musicOn.activeSelf)
        {
            musicOff.SetActive(true);
            musicOn.SetActive(false);
            AudioGamePlayManager.Instance.StopMusic();
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 0);
        }
        else
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
            AudioGamePlayManager.Instance.ContinueMusic();
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 1);
        }
    }
}
