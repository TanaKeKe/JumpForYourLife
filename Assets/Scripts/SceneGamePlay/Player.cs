using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float force;

    [Space(10)]
    private Rigidbody2D _rigidbody2d;
    
    private Collider2D _collider2D;
    
    
    private bool _isJump;
    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        
        
        _collider2D = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isJump == false)
        {
            Debug.Log("Ấn vào màn hình");
            Messenger.Broadcast(EventKey.PlayerJump);// Phát sự kiện JUMP
            _isJump = true;
            if(_isJump)
            {
                _rigidbody2d.AddForce(Vector2.up * force);
            }
            
            
        }
        
        
        

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            transform.SetParent(null);
            Debug.Log("BLock va chạm nhân vật nè");
            setConnectBlock(collision);
        }

        
    }

    private void setConnectBlock(Collision2D collision)
    {
       
        _isJump = false;
        // collion là khối player đang va chạm => cho khối player đang va chạm làm bố của nhân vật
        transform.SetParent(collision.transform);
        
    }
}
