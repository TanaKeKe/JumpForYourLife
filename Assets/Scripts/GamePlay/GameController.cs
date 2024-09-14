using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : Singleton<GameController>
{
    [SerializeField] private Camera myCamera; // cameraController
    [SerializeField] private GameObject bars;
    [SerializeField] private float rangeCamera;
    [SerializeField] private float countChangeCamera;
    [Space(10)]

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float spaceBetweenTwoBlocks;
    [SerializeField] private GameObject perfect;
    [Space(10)]

    [SerializeField] private GameObject tutorial;
    private int _score;
    private Vector3 _targetPosition;
    public bool _isPlaying;
    public bool _isPerfect;
    public bool _isFinish;
    
    private void Start()
    {
        _score = 0;
        scoreText.text = _score.ToString();
    }

    private void Update()
    {
        Play();
        if (_isFinish)
        {
            // show popup
        }

    }


    private void Play()
    {
        if (_isPlaying)
        {
            tutorial.SetActive(false);
        }
    }
    private void OnEnable()
    {
        Messenger.AddListener<float>(EventKey.Score, UpdateScore);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<float>(EventKey.Score, UpdateScore);
    }

    public void UpdateScore(float distance)
    {

        int currentScore = (int)distance / (int)spaceBetweenTwoBlocks;
        if (_isPerfect)
        {
            currentScore *= 2;
            //_isPerfect = false;
        }
        _score += currentScore;
        scoreText.text = _score.ToString();
    }

    public void SetActivePerfect()
    {
        StartCoroutine(CoroutinePerfect());
    }
    private IEnumerator CoroutinePerfect()
    {
        perfect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        perfect.SetActive(false);
    }
    public void ChangePositionCamera(float distanceJump)
    {
        Debug.Log("Thay đổi camera" + distanceJump);
        _targetPosition = myCamera.transform.position + Vector3.down * (distanceJump);
        StartCoroutine(CoroutineSmooth());

    }

    private IEnumerator CoroutineSmooth()
    {
        while (myCamera.transform.position.y > _targetPosition.y)
        {
            myCamera.transform.position += Vector3.down * countChangeCamera;
            if (myCamera.transform.position.y == _targetPosition.y) break;
            yield return null;
        }
    }

    public float GetRangeTopCamera()
    {
        return myCamera.transform.position.y + rangeCamera;
    }

    public float GetRangeBottomCamera()
    {
        return myCamera.transform.position.y - rangeCamera;
    }

    public Camera GetMyCamera()
    {
        return myCamera;
    }
}
