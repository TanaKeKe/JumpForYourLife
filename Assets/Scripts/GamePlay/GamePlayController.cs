using System.Collections;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayController : Singleton<GamePlayController>
{
    [Header("-----------Camera----------")]
    [SerializeField] private Camera myCamera;
    [SerializeField] private GameObject bars;
    [SerializeField] private float rangeCamera;
    [SerializeField] private float countChangeCamera;

    [Header("----------Other----------")]
    [SerializeField] private float spaceBetweenTwoBlocks;
    [SerializeField] private GameObject perfect;
    [SerializeField] private GameObject countDown;
    [SerializeField] private GameObject endGameBar;

    [Header("----------Public----------")]
    public int score;
    public bool isPlaying;
    public bool isPerfect;
    public bool isFinish;
    public bool isPause;
    public bool isResume;
    public bool isStart;

    private Vector3 _targetPosition;

    public float SpaceBetweenTwoBlocks {  get { return spaceBetweenTwoBlocks; } }
    
    private void Start()
    {
        Application.targetFrameRate = GameConfig.FPS;
        LoadPlayer();
        countDown.transform.SetParent(GamePlayController.Instance.GetCamera().transform);
        perfect.transform.SetParent(GamePlayController.Instance.GetCamera().transform);
        endGameBar.transform.SetParent(GamePlayController.Instance.GetCamera().transform);
        score = 0;
    }

    private void LoadPlayer()
    {
        string namePlayer = PlayerPrefs.GetString(GamePrefs.PLAYER_KEY);
        GameObject playerCurrent = Resources.Load<GameObject>(GameConfig.PLAYER_PATH + namePlayer);
        Instantiate(playerCurrent, transform);
    }

    private void OnEnable()
    {
        Messenger.AddListener<float>(EventKey.UpdateScore, UpdateScore);
        Messenger.AddListener<TextMeshProUGUI>(EventKey.ShowScore, ShowScore);
        Messenger.AddListener(EventKey.Replay, ReloadScenePlay);
        Messenger.AddListener(EventKey.GoHome, ReLoadSceneHome);
    }

    
    private void OnDisable()
    {
        Messenger.RemoveListener<float>(EventKey.UpdateScore, UpdateScore);
        Messenger.RemoveListener<TextMeshProUGUI>(EventKey.ShowScore, ShowScore);
        Messenger.RemoveListener(EventKey.Replay, ReloadScenePlay);
        Messenger.RemoveListener(EventKey.GoHome, ReLoadSceneHome);
    }

    private void ReLoadSceneHome()
    {
        if(isFinish)
        {
            SaveGame.SaveHighScore(score);
        }
        SceneManager.LoadScene(Scene.home);
    }

    private void Update()
    {
        CheckPause();
        CheckResume();
    }

    private void CheckPause()
    {
        if (isPause == false && countDown.activeInHierarchy == false)
        {
            Messenger.Broadcast(EventKey.PlayerJump);
        }
    }
    private void CheckResume()
    {
        if (isResume)
        {
            StartCoroutine(CoroutineResume());
            isResume = false;
        }
    }

    private IEnumerator CoroutineResume()
    {
        countDown.SetActive(true);
        yield return new WaitForSeconds(3f);
        countDown.SetActive(false);
        Messenger.Broadcast(EventKey.SetSpeedBlocks);
    }

    private void ReloadScenePlay()
    {
        if (isFinish)
        {
            SaveGame.SaveHighScore(score);
        }
        SceneManager.LoadScene(Scene.gameplay);
    }

    private void ShowScore(TextMeshProUGUI scoreText)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(float distance)
    {
        int currentScore = (int)distance / (int)spaceBetweenTwoBlocks;
        if (isPerfect)
        {
            currentScore *= 2;
        }
        score += currentScore;
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
        //Debug.Log("Thay đổi camera" + distanceJump);
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
        Messenger.Broadcast(EventKey.GetBlockFormPool);
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