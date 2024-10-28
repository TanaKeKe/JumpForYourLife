using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockEvent blockEvent; 
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange;

    public BlockEvent BlockEvent => blockEvent;

    public Collider2D _collider2D;

    private float _leftLimit;
    private float _rightLimit;
    private float _angle;
    private float _pauseSpeed;
    private float _speedBosst;

    private void Start()
    {
        _angle = 0f;
        _leftLimit = -2.1f;
        _rightLimit = 2.1f;
        _speedBosst = 0.4f;
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
        transform.Translate(Vector3.right * speed * Time.deltaTime + Vector3.up * _angle * Time.deltaTime);
    }

    public void SetAngle()
    {
        if(speed > 0)
        {
            _angle = (float)Random.Range(0.1f, 0.25f);
        }
        else
        {
            _angle = (float)Random.Range(-0.25f, -0.1f);
        }
    }

    public void SetNoneAngle()
    {
        _angle = 0f;
    }

    public void ChangeDirection()
    {
        if (transform.position.x < _leftLimit)
        {
            Vector3 position = transform.position;
            position.x = _leftLimit;
            transform.position = position;
        }

        if (transform.position.x > _rightLimit)
        {
            Vector3 position = transform.position;
            position.x = _rightLimit;
            transform.position = position;
        }

        // không so sánh 2 số float vì sẽ có sai số cực nhỏ
        if (Mathf.Approximately(Math.Abs(transform.position.x), _rightLimit))
        {
            speed *= -1f;
            _angle *= -1f;
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
        _angle = 0;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetLimitNormalBlock()
    {
        _leftLimit = -2.1f;
        _rightLimit = 2.1f;
    }

    public void SetLimitMediumBlock()
    {
        _leftLimit = -2.2f;
        _rightLimit = 2.2f;
    }

    public void SetLimitHardBlock()
    {
        _leftLimit = -2.3f;
        _rightLimit = 2.3f;
    }

    public void BoostSpeed()
    {
        if(speed < 0)
        {
            if(speed > -2f)
            {
                speed -= _speedBosst;
            }
        }
        else
        {
            if(speed < 2f)
            {
                speed += _speedBosst;
            }
        }
    }
}