using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange = 0.1f;
    
    
    private float _pauseSpeed;
    public Collider2D _collider2D;
    public float leftLimit = -2.1f;
    public float rightLimit = 2.1f;
    public float angle = 0f;
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
        transform.Translate(Vector3.right * speed * Time.deltaTime + Vector3.up * angle * Time.deltaTime);
    }

    public void SetAngle()
    {
        if(speed > 0)
        {
            angle = (float)Random.Range(0.1f, 0.15f);
        }
        else
        {
            angle = (float)Random.Range(-0.15f, -0.1f);
        }
    }

    public void SetNoneAngle()
    {
        angle = 0f;
    }

    public void ChangeDirection()
    {
        if (transform.position.x < leftLimit)
        {
            Vector3 position = transform.position;
            position.x = leftLimit;
            transform.position = position;
        }

        if (transform.position.x > rightLimit)
        {
            Vector3 position = transform.position;
            position.x = rightLimit;
            transform.position = position;
        }

        // không so sánh 2 số float vì sẽ có sai số cực nhỏ
        if (Mathf.Approximately(Math.Abs(transform.position.x), rightLimit))
        {
            speed *= -1f;
            angle *= -1f;
        }
    }

    public void RandomSpeed(int direction)
    {
        speed = direction * Random.Range(speed - speedRandomRange, speed + speedRandomRange);
    }

    public void SetNoneSpeed()
    {
        _pauseSpeed = speed;
        speed = 0;
        angle = 0;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLimitNormalBlock()
    {
        leftLimit = -2.1f;
        rightLimit = 2.1f;
    }

    public void SetLimitMediumBlock()
    {
        leftLimit = -2.2f;
        rightLimit = 2.2f;
    }

    public void SetLimitHardBlock()
    {
        leftLimit = -2.3f;
        rightLimit = 2.3f;
    }

    public void BoostSpeed()
    {
        if(speed < 0)
        {
            if(speed > -2f)
            {
                speed -= 0.4f;
            }
        }
        else
        {
            if(speed < 2f)
            {
                speed += 0.4f;
            }
        }
    }
}