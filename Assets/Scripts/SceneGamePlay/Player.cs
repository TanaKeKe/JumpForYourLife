using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private Transform _transform;
    private Collider2D _collider2D;
    [SerializeField] private float speed;
    private bool _isJump;
    private bool _checkDelayJump;
    [SerializeField] private float _lengthDelayJump;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ấn vào màn hình");
            _isJump = true;
            Messenger.Broadcast(EventKey.JUMP); // Phát sự kiện JUMP
            _transform.parent = null; // trả lại tranform hiện tại của player
            StartCoroutine(CoroutineJump());
            
        }

        if (_isJump && _checkDelayJump)
        {
            Jump();
        }
    }

    private IEnumerator CoroutineJump()
    {
        yield return new WaitForSeconds(_lengthDelayJump);
        _checkDelayJump = true;
    }

    private void Jump()
    {
        if(_isJump)
        {
            _transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("BLock va chạm nhân vật nè");
            setConnectBlock(collision);
        }

        if( collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Tường va chạm nhân vật nè");
            Messenger.Broadcast(EventKey.COLLIDER);
        }
    }

    private void setConnectBlock(Collision2D collision)
    {
        // collion là khối player đang va chạm => cho khối player đang va chạm làm bố của nhân vật
        _transform.parent = collision.transform;
        // khi chạm vào đất thì _isJump bằng false
        _isJump = false;
    }
}
