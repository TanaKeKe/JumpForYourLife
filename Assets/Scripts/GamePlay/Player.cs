using System;
using UnityEngine;

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
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isJump == false)
        {
            
            _startGame = true;
            Debug.Log("Ấn vào màn hình");
            Messenger.Broadcast(EventKey.PlayerJump); // Phát sự kiện JUMP
            _isJump = true;
            if (_isJump)
            {
                _positionStartJump = transform.position.y;
                transform.SetParent(null);
                _rigidbody2d.AddForce(Vector2.up * force);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("BLock va chạm nhân vật nè");
            setConnectBlock(collision);
            SetDistanceJump();
            if (_startGame)
            {
                GameController.Instance.ChangePositionCamera(_distanceJump);
                GameController.Instance.ChangePositionBars(_distanceJump);
            }
        }
    }
    

    private void setConnectBlock(Collision2D collision)
    {
        _isJump = false;
        // collion là khối player đang va chạm => cho khối player đang va chạm làm bố của nhân vật
        transform.SetParent(collision.transform);
    }

    public void SetDistanceJump()
    {
        _positionEndJump = transform.position.y;
        _distanceJump = Math.Abs(_positionEndJump - _positionStartJump);
        _distanceJump = (float)Math.Round(_distanceJump,1);
    }
}