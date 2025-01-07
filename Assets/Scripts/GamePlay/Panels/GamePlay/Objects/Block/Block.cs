using System;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockEvent blockEvent;
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange;
    [SerializeField] private Transform positionWallLeft;
    [SerializeField] private Transform positionWallRight;

    public BlockEvent BlockEvent => blockEvent;

    public Collider2D _collider2D;
    private float _angle;
    private float _pauseSpeed;
    private float _speedBosst;

    private void Start()
    {
        _angle = 0f;
        _speedBosst = 0.4f;
        Messenger.Broadcast(EventKey.GetBlockFromPool);
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
        CheckPauseAndFinish();
    }

    private void CheckPauseAndFinish()
    {
        if (GamePlayController.Instance.isPause || GamePlayController.Instance.isFinish)
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
        if (speed > 0)
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
        speed *= -1f;
        _angle *= -1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameTags.WALL_TAG))
        {
            ChangeDirection();
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

    public void BoostSpeed()
    {
        if (speed < 0)
        {
            if (speed > -2f)
            {
                speed -= _speedBosst;
            }
        }
        else
        {
            if (speed < 2f)
            {
                speed += _speedBosst;
            }
        }
    }
}