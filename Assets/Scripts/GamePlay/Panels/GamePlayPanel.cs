using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

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
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + PlayerPrefs.GetString("theme"));
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
        PanelManager.Instance.OpenPanel("PauseGamePanel");
    }
}
