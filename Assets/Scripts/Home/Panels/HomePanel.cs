using Common;
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
        if (!PlayerPrefs.HasKey(GamePrefs.PLAYER_KEY))
        {
            PlayerPrefs.SetString(GamePrefs.PLAYER_KEY, GameConfig.PLAYER_NAME);
        }

        if (!PlayerPrefs.HasKey(GamePrefs.THEME_KEY))
        {
            PlayerPrefs.SetString(GamePrefs.THEME_KEY, GameConfig.THEME_FALL);
        }

        highScoreText.text = PlayerPrefs.GetInt(GamePrefs.HIGH_SCORE_KEY, 0).ToString();
        SetOriginIconShop();
    }

    private void SetOriginIconShop()
    {
        PlayerInfors[] playerInfors = Resources.LoadAll<PlayerInfors>(GameConfig.PLAYER_INFORS_PATH);
        foreach (PlayerInfors obj in playerInfors)
        {
            if (GamePrefs.GetPlayerName().Equals(obj.AvatarName))
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
        PanelManager.Instance.OpenPanel(GameConfig.SETTING_PANEL);
    }

    public void OpenShop()
    {
        PanelManager.Instance.OpenPanel(GameConfig.SHOP_PANEL);
    }
}