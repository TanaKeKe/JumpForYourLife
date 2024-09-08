using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedRandomRange = 0.2f;

    private Collider2D _collider2D;

    private void Awake()
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
            ChangeDirection();
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Wall"))
    //     {
    //         Debug.Log("Đất va chạm vào tường nè bà");
    //         ChangeDirection();
    //     }
    // }

    public void ChangeDirection()
    {
        speed *= -1f;
    }

    public void RandomSpeed(int direction)
    {
        speed = direction * Random.Range(speed - speedRandomRange, speed + speedRandomRange);
    }
}