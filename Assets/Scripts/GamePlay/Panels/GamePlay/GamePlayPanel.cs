﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Common;

public class GamePlayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private Image backGround;

    private int _score;

    private void Start()
    {
        LoadBackGround();
        tutorial.SetActive(true);
        _score = 0;
        scoreText.text = _score.ToString();
    }

    private void LoadBackGround()
    {
        ThemeInfors theme = Resources.Load<ThemeInfors>( GameConfig.THEME_INFOR_PATH + "/" + GamePrefs.GetThemeOriginName());
        backGround.sprite = theme.BackgroundSprite;
    }

    private void Update()
    {
        SetActiveTutorial();
        Messenger.Broadcast<TextMeshProUGUI>(EventKey.ShowScore, scoreText);
    }

    private void SetActiveTutorial()
    {
        if (GamePlayController.Instance.isPlaying)
        {
            tutorial.SetActive(false);
        }
    }

    public void Pause()
    {
        GamePlayController.Instance.isPause = true;
        GamePlayController.Instance.isResume = false;
        //Debug.Log("Đã pause");
        PanelManager.Instance.OpenPanel(GameConfig.PAUSE_PANEL);
    }
}
