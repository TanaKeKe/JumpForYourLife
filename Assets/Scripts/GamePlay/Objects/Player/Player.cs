using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float force;

    private Rigidbody2D _rigidbody2d;
    private Collider2D _collider2D;
    private Animator _animator;
    private bool _isJump;
    private float _distanceJump;
    private bool _startGame;
    private float _positionYPlayer;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _positionYPlayer = 3.5f;
    }
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetNullParentOfPlayer, SetNullParent);
        Messenger.AddListener(EventKey.PlayerJump, Jump);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetNullParentOfPlayer, SetNullParent);
        Messenger.RemoveListener(EventKey.PlayerJump, Jump);
    }

    private void SetNullParent()
    {
        transform.SetParent(null);
        _animator.SetBool("isJumping", true);
        _isJump = true;
    }

    public void Jump()
    {
        if (GamePlayController.Instance.isStart && Input.GetMouseButtonDown(0) && !UIHelper.IsMouseOverUI())
        {
            if (!_isJump)
            {
                GamePlayController.Instance.isPlaying = true;
                //Debug.Log("Nhân vật nhảy");
                SetNullParent();
                AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.JumpSound);
                Messenger.Broadcast(EventKey.SetStatusBlockWhenPlay); 
                _rigidbody2d.AddForce(Vector2.up * force);
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            _animator.SetBool("isJumping", false);
            _isJump = false;
            SetActionColliderWithBlockWhenPlaying(collision);
        }
    }

    private void SetActionColliderWithBlockWhenPlaying(Collision2D collision)
    {
        if (GamePlayController.Instance.isPlaying)
        {
            SetDistanceJump();
            setConnectBlock(collision);
            Messenger.Broadcast(EventKey.MoveWall);
            GamePlayController.Instance.ChangePositionCamera(_distanceJump);
            Messenger.Broadcast<float>(EventKey.UpdateScore, _distanceJump);
            GamePlayController.Instance.isPerfect = false;
        }
        else
        {
            GamePlayController.Instance.isStart = true;
        }
    }

    private void setConnectBlock(Collision2D collision)
    {
        transform.SetParent(collision.transform); // block làm bố nhân vật
        PerfectAction();
    }

    private void PerfectAction()
    {
        Vector3 localPosition = transform.localPosition;
        //Debug.Log("Tọa độ X của Player là: " + localPosition.x);
        if (localPosition.x <= 0.1f && localPosition.x >= -0.1f)
        {
            GamePlayController.Instance.isPerfect = true;
            GamePlayController.Instance.SetActivePerfect();
        }
    }

    public void SetDistanceJump()
    {
        double distanceJumpCurrent = Math.Round(_positionYPlayer - transform.position.y, 1);
        if (distanceJumpCurrent < 4.5f) distanceJumpCurrent = 3.5f;
        //Debug.Log(distanceJumpCurrent);
        if(Mathf.Approximately((float)distanceJumpCurrent,3.5f) == true)
        {
            _distanceJump = 3.5f;
            _positionYPlayer = transform.position.y;
        }
        else
        {
            _distanceJump = 7f;
            _positionYPlayer = transform.position.y;
        }
    }
}