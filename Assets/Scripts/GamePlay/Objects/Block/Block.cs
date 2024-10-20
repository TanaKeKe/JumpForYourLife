﻿using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange = 0.1f;
    

    private float _pauseSpeed;
    public Collider2D _collider2D;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetSpeedBlocks, SetSpeedBlocks);
    }


    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetSpeedBlocks, SetSpeedBlocks);
    }

    private void SetSpeedBlocks()
    {
        speed = _pauseSpeed;
    }

    private void Update()
    {
        Move();
        ChangeDirection();
        CheckPauseAndFinish();
    }

    private void CheckPauseAndFinish()
    {
        if(GamePlayController.Instance.isPause || GamePlayController.Instance.isFinish)
        {
            if (speed != 0)
            {
                SetNoneSpeed();
            }
        }
    }

    private void Move()
    {
        // transform.position += Vector3.right * speed * Time.deltaTime;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void ChangeDirection()
    {
        if (transform.position.x < -2.1f)
        {
            Vector3 position = transform.position;
            position.x = -2.1f;
            transform.position = position;
        }

        if (transform.position.x > 2.1f)
        {
            Vector3 position = transform.position;
            position.x = 2.1f;
            transform.position = position;
        }

        // không so sánh 2 số float vì sẽ có sai số cực nhỏ
        if (Mathf.Approximately(Math.Abs(transform.position.x), 2.1f)) speed *= -1f;
    }

    public void RandomSpeed(int direction)
    {
        speed = direction * Random.Range(speed - speedRandomRange, speed + speedRandomRange);
    }

    public void SetNoneSpeed()
    {
        _pauseSpeed = speed;
        speed = 0;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}