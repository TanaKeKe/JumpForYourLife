using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject glow;
    [SerializeField] private GameObject scoreImage;
    [SerializeField] private GameObject newRecordImage;

    private Quaternion _quaternion;
    private float _rotationZ;
    private void Start()
    {
        Messenger.Broadcast<TextMeshProUGUI>(EventKey.ShowScore, scoreText);
        ViewImageScore();
    }

    private void ViewImageScore()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) > int.Parse(scoreText.text))
        {
            scoreImage.SetActive(true);
            newRecordImage.SetActive(false);
        }
        else
        {
            scoreImage.SetActive(false);
            newRecordImage.SetActive(true);
        }
    }

    private void Update()
    {
        RotationGlow();
    }

    private void RotationGlow()
    {
        _quaternion = Quaternion.Euler(0, 0, _rotationZ++);
        glow.transform.rotation = _quaternion;
    }

    public void Replay()
    {
        Messenger.Broadcast(EventKey.Replay);
    }

    public void GoBackHome()
    {
        Messenger.Broadcast(EventKey.GoHome);
    }
}
