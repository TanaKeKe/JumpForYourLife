using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGamePanel : Panel
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject glow;

    private Quaternion _quaternion;
    private float _rotationZ;
    private void Start()
    {
        Messenger.Broadcast<TextMeshProUGUI>(EventKey.ShowScore, scoreText);
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
        Debug.Log("Đã chạm vào nút replay");
        Messenger.Broadcast(EventKey.Replay);
    }
}
