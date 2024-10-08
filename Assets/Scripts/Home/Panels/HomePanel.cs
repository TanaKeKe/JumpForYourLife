using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePanel : Panel
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Image iconShop;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("player"))
        {
            PlayerPrefs.SetString("player", "TanaKeKe");
        }

        if (!PlayerPrefs.HasKey("theme"))
        {
            PlayerPrefs.SetString("theme", "Fall");
        }

        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        SetOriginIconShop();
    }

    private void SetOriginIconShop()
    {
        PlayerInfors[] playerInfors = Resources.LoadAll<PlayerInfors>("ScriptableObjects/PlayerInfors");
        foreach(PlayerInfors obj in playerInfors)
        {
            if (PlayerPrefs.GetString("player").Equals(obj.AvatarName))
            {
                iconShop.sprite = obj.AvatarSpriteOn;
                break;
            }
        }
    }

    private void Update()
    {
        Messenger.Broadcast(EventKey.SetIconShop, iconShop);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(Scene.gameplay);
    }

    public void OpenSetting()
    {
        PanelManager.Instance.OpenPanel("SettingPanel");
    }

    public void OpenShop()
    {
        PanelManager.Instance.OpenPanel("ShopPanel");
    }
}
