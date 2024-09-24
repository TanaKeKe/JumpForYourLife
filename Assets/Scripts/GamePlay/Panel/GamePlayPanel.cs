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
    private int _score;
    private void Start()
    {
        tutorial.SetActive(true);
        _score = 0;
        scoreText.text = _score.ToString();
    }

    private void Update()
    {
        SetActiveTutorial();
        Messenger.Broadcast<TextMeshProUGUI>(EventKey.ShowScore,scoreText);
    }

    private void SetActiveTutorial()
    {
        if(GamePlayController.Instance.isPlaying)
        {
            tutorial.SetActive(false);
        }
    }

    public void Pause()
    {
        GamePlayController.Instance.isPause = true;
        Debug.Log("Đã pause");
        PanelManager.Instance.OpenPanel("PauseGamePanel");
    }
}
