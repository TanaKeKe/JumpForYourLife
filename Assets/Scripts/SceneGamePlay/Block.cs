using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform _transform;
    private bool _checkCollisionWithWall;
    private Collider2D _collider2D;
    private void Awake()
    {
        _transform = transform;
        _collider2D = GetComponent<Collider2D>();
    }
    private void Start()
    {
        Messenger.AddListener(EventKey.JUMP, turnOnTrigger);
        Messenger.AddListener(EventKey.COLLIDER, CheckCollisionWithWall);
    }

    private void CheckCollisionWithWall()
    {
        if (_checkCollisionWithWall)
        {
            speed *= -1f;
            _checkCollisionWithWall = false;
        }
    }

    private void Update()
    {
        Move();
       
    }

    private void turnOnTrigger()
    {
        _collider2D.isTrigger = true;
        speed = 0f;
    }
    
    private void Move()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
        CheckCollisionWithWall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            
            _checkCollisionWithWall = true;
            Debug.Log("Đất va chạm vào tường nè bà");
        }
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.JUMP, turnOnTrigger);
        Messenger.RemoveListener(EventKey.COLLIDER, CheckCollisionWithWall);
    }
}
