using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float force;

    private Rigidbody2D _rigidbody2d;
    private Collider2D _collider2D;
    private bool _isJump;
    private float _positionStartJump;
    private float _positionEndJump;
    private float _distanceJump;
    private bool _startGame;

    private void Awake()
    {
        _positionStartJump = 3f;
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetNullParentOfPlayer, SetNullParent);
        Messenger.AddListener(EventKey.PlayerJump,Jump);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetNullParentOfPlayer, SetNullParent);
        Messenger.RemoveListener(EventKey.PlayerJump, Jump);
    }

    private void SetNullParent()
    {
        transform.SetParent(null);
    }

    public void Jump()
    {
        if (GamePlayController.Instance.isStart && Input.GetMouseButtonDown(0) && !UIHelper.IsMouseOverUI())
        {
            if (!_isJump)
            {
                GamePlayController.Instance.isPlaying = true;
                Messenger.Broadcast(EventKey.PlayerOnTriggerBlock);
                //Debug.Log("Nhân vật nhảy");
                _isJump = true;
                if (_isJump)
                {
                    SetNullParent();
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.JumpSound);
                    _rigidbody2d.AddForce(Vector2.up * force);
                }
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            GamePlayController.Instance.isStart = true;
            //Debug.Log("BLock va chạm nhân vật nè");
            setConnectBlock(collision);
            SetDistanceJump();
            if (GamePlayController.Instance.isPlaying)
            {
                GamePlayController.Instance.ChangePositionCamera(_distanceJump);
                GamePlayController.Instance.ChangePositionBars(_distanceJump);
                Messenger.Broadcast<float>(EventKey.UpdateScore, _distanceJump);
                GamePlayController.Instance.isPerfect = false;
            }
            
        }
    }

    private void setConnectBlock(Collision2D collision)
    {
        _isJump = false;
        // collion là khối player đang va chạm => cho khối player đang va chạm làm bố của nhân vật
        transform.SetParent(collision.transform);
        if (GamePlayController.Instance.isPlaying)
        {
            Vector3 localPosition = transform.localPosition; // vị trí hiện tại của player ngay khi va chạm
            Debug.Log("Tọa độ X của Player là: " + localPosition.x);
            if (localPosition.x <= 0.1f && localPosition.x >= -0.1f)
            {
                Debug.Log("Nhay vao perfect");
                GamePlayController.Instance.isPerfect = true;
                GamePlayController.Instance.SetActivePerfect();
            }
        }
    }

    public void SetDistanceJump()
    {
        _positionEndJump = transform.position.y;
        _distanceJump = Math.Abs(_positionEndJump - _positionStartJump);
        _distanceJump = (float)Math.Round(_distanceJump, 1);
        _positionStartJump = _positionEndJump;
    }
}