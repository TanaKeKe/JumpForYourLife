using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform glow;

    [SerializeField] private SpriteRenderer glowSR;
    [SerializeField] private GameObject scoreImage;
    [SerializeField] private GameObject newRecordImage;

    private Quaternion _quaternion;
    private float _rotationZ;

    private void Start()
    {
        Messenger.Broadcast<TextMeshProUGUI>(EventKey.ShowScore, scoreText);
        ViewImageScore();

        glow.DOKill();
        glow.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) // Quay đều
            .SetLoops(-1, LoopType.Restart); // Lặp vô tận
    }

    private void ViewImageScore()
    {
        if (PlayerPrefs.GetInt(GamePrefs.HIGH_SCORE_KEY, 0) > int.Parse(scoreText.text))
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
        // RotationGlow();
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