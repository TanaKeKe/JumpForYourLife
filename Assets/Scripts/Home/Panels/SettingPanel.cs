using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    public void CheckSound()
    {
        if (soundOn.activeInHierarchy)
        {
            soundOff.SetActive(true);
            soundOn.SetActive(false);
            AudioHomeManager.Instance.StopSound();
            PlayerPrefs.SetString("soundState", "OFF");
        }
        else
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            AudioHomeManager.Instance.ContinueSound();
            PlayerPrefs.SetString("soundState", "ON");
        }
    }

    public void CheckMusic()
    {
        if (musicOn.activeInHierarchy)
        {
            musicOff.SetActive(true);
            musicOn.SetActive(false);
            AudioHomeManager.Instance.StopMusic();
            PlayerPrefs.SetString("musicState", "OFF");
        }
        else
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
            AudioHomeManager.Instance.ContinueMusic();
            PlayerPrefs.SetString("musicState", "ON");
        }
    }

    public void ClosePanel()
    {
        popup.transform.DOScale(0, 0.5f).SetEase(Ease.InQuart);
        image.color = new Color(1,1,1,0);
        StartCoroutine(CouroutinePopup());
    }

    private IEnumerator CouroutinePopup()
    {
        yield return new WaitForSeconds(0.5f);
        PanelManager.Instance.ClosePanel("SettingPanel");
    }

    public void OpenLink()
    {
        UnityEngine.Debug.Log("Ấn vào nút open");

        // Liên kết Facebook cần mở
        string url = "https://www.facebook.com/tan.phanthanh.731";

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
