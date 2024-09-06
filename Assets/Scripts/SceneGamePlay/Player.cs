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
    [SerializeField] private float jumpForce;
    private bool _isJump;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _rigidbody2d.velocity = Vector3.zero;
        _transform = GetComponent<Transform>();
        _collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isJump == false)
        {
            Debug.Log("Ấn vào màn hình");
            Messenger.Broadcast(EventKey.JUMP);// Phát sự kiện JUMP
            _isJump = true;
            if(_isJump)
            {
                _rigidbody2d.velocity = Vector3.up * jumpForce;
            }
            _transform.SetParent(null); // trả lại tranform hiện tại của player
            
        }
        
        
        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            _isJump = false;
            Debug.Log("BLock va chạm nhân vật nè");
            setConnectBlock(collision);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Tường va chạm nhân vật nè");
            
        }
    }

    private void setConnectBlock(Collision2D collision)
    {
        // collion là khối player đang va chạm => cho khối player đang va chạm làm bố của nhân vật
        _transform.parent = collision.transform;
        
    }
}
