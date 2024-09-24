using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class GamePlayController : Singleton<GamePlayController>
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private GameObject bars;
    [SerializeField] private float rangeCamera;
    [SerializeField] private float countChangeCamera;
    [Space(10)]

    [SerializeField] private float spaceBetweenTwoBlocks;
    [SerializeField] private GameObject perfect;

    private int _score;
    private Vector3 _targetPosition;
    public bool isPlaying;
    public bool isPerfect;
    public bool isFinish;
    public bool isPause;

    private void Start()
    {
        perfect.transform.SetParent(GamePlayController.Instance.GetCamera().transform);
        _score = 0;
    }

    private void OnEnable()
    {
        Messenger.AddListener<float>(EventKey.UpdateScore, UpdateScore);
        Messenger.AddListener<TextMeshProUGUI>(EventKey.ShowScore, ShowScore);
        Messenger.AddListener(EventKey.Replay, ReloadScene);
    }

    
    private void OnDisable()
    {
        Messenger.RemoveListener<float>(EventKey.UpdateScore, UpdateScore);
        Messenger.RemoveListener<TextMeshProUGUI>(EventKey.ShowScore, ShowScore);
        Messenger.RemoveListener(EventKey.Replay, ReloadScene);
    }

    private void Update()
    {
        if (isPause == false)
        {
            Debug.Log("Ấn để chơi game");
            Messenger.Broadcast(EventKey.PlayerJump);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(Scene.gameplay);
    }

    private void ShowScore(TextMeshProUGUI scoreText)
    {
        scoreText.text = _score.ToString();
    }

    public void UpdateScore(float distance)
    {
        int currentScore = (int)distance / (int)spaceBetweenTwoBlocks;
        if (isPerfect)
        {
            currentScore *= 2;
        }
        _score += currentScore;
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

    public void ChangePositionBars(float distanceJump)
    {
        Debug.Log("Thay đổi vị trí của 2 thanh tắt trigger" + bars.transform.position);
        bars.transform.position += (Vector3.down * distanceJump);
    }

    public float GetRangeTopCamera()
    {
        return myCamera.transform.position.y + rangeCamera;
    }

    public float GetRangeBottomCamera()
    {
        return myCamera.transform.position.y - rangeCamera;
    }

    public Camera GetCamera()
    {
        return myCamera;
    }
}