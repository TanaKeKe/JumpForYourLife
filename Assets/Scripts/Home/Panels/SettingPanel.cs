using System;
using System.Collections;
using System.Diagnostics;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : Panel
{
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject popup;
    [SerializeField] private Image image;

    private void Start()
    {
        popup.transform.DOScale(1, 1f).SetEase(Ease.OutQuart);
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
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 1);
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

    public void CheckSound()
    {
        if (soundOn.activeSelf)
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            AudioHomeManager.Instance.StopSound();
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 0);
        }
        else
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            AudioHomeManager.Instance.ContinueSound();
            PlayerPrefs.SetInt(GamePrefs.SOUND_KEY, 1);
        }
    }

    public void CheckMusic()
    {
        if (musicOn.activeSelf)
        {
            musicOff.SetActive(true);
            musicOn.SetActive(false);
            AudioHomeManager.Instance.StopMusic();
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 0);
        }
        else
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
            AudioHomeManager.Instance.ContinueMusic();
            PlayerPrefs.SetInt(GamePrefs.MUSIC_KEY, 1);
        }
    }

    public void ClosePanel()
    {
        popup.transform.DOScale(0, 0.5f).SetEase(Ease.InQuart);
        image.color = new Color(1, 1, 1, 0);
        StartCoroutine(IEDelayClosePopup());
    }

    private IEnumerator IEDelayClosePopup()
    {
        yield return new WaitForSeconds(0.5f);
        PanelManager.Instance.ClosePanel(GameConfig.SETTING_PANEL);
    }

    public void OpenLink()
    {
        UnityEngine.Debug.Log("Ấn vào nút open");

        // Liên kết Facebook cần mở
        string url = GameConfig.FB_URL;

        // Kiểm tra hệ điều hành (để xử lý theo cách tương thích)
        try
        {
            // Đối với Windows
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            // Xử lý ngoại lệ nếu không thể mở URL
            Console.WriteLine("Không thể mở URL: " + e.Message);
        }
    }
}