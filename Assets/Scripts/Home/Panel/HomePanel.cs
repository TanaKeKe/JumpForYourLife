using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomePanel : Panel
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    private void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Scene.gameplay);
    }

    public void OpenSetting()
    {
        PanelManager.Instance.OpenPanel("SettingPanel");
    }
}
