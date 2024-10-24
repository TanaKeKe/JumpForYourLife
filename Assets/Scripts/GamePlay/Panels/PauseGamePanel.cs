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
        if (PlayerPrefs.HasKey("soundState"))
        {
            if (PlayerPrefs.GetString("soundState").Equals("ON"))
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
            PlayerPrefs.SetString("soundState", "ON");
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
    }

    private void LoadDataMusic()
    {
        if (PlayerPrefs.HasKey("musicState"))
        {
            if (PlayerPrefs.GetString("musicState").Equals("ON"))
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
            PlayerPrefs.SetString("musicState", "ON");
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
        if (soundOn.activeInHierarchy)
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            AudioGamePlayManager.Instance.StopSound();
            PlayerPrefs.SetString("soundState", "OFF");
        }
        else
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            AudioGamePlayManager.Instance.ContinueSound();
            PlayerPrefs.SetString("soundState", "ON");
        }
    }

    public void CheckMusic()
    {
        if(musicOn.activeInHierarchy)
        {
            musicOff.SetActive(true);
            musicOn.SetActive(false);
            AudioGamePlayManager.Instance.StopMusic();
            PlayerPrefs.SetString("musicState", "OFF");
        }
        else
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
            AudioGamePlayManager.Instance.ContinueMusic();
            PlayerPrefs.SetString("musicState", "ON");
        }
    }
}
