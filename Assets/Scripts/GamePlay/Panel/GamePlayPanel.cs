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
    [SerializeField] private Button pauseGame;
    private int _score;
    public bool isPause;
    private void Start()
    {
        tutorial.SetActive(true);
        _score = 0;
        scoreText.text = _score.ToString();
        pauseGame.onClick.AddListener(Pause);
    }

    private void Update()
    {
        if(isPause == false)
        {
            Debug.Log("Ấn để chơi game");
            Messenger.Broadcast(EventKey.PlayerJump);
        }

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
        isPause = true;
        Debug.Log("Đã pause");
        PanelManager.Instance.OpenPanel("PauseGamePanel");
    }
}
