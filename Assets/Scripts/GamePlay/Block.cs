using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange = 0.1f;

    private Collider2D _collider2D;

    private void Start()
    {
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.PlayerJump, TurnOnTrigger);
        Messenger.AddListener(EventKey.PlayerConnectBlock, TurnOffTrigger);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.PlayerJump, TurnOnTrigger);
        Messenger.RemoveListener(EventKey.PlayerConnectBlock, TurnOffTrigger);
    }

    private void TurnOnTrigger()
    {
        _collider2D.isTrigger = true;
    }

    private void TurnOffTrigger()
    {
        _collider2D.isTrigger = false;
    }

    private void Update()
    {
        Move();
        ChangeDirection();
        CheckFinish();
    }

    private void CheckFinish()
    {
        if(GamePlayController.Instance.isFinish)
        {
            SetNoneSpeed();
        }
    }

    private void Move()
    {
        // transform.position += Vector3.right * speed * Time.deltaTime;
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Đất va chạm vào tường nè bà");
        }
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
        speed = 0;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}