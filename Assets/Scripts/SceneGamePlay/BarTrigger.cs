using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Nhân vật va chạm với thanh bật trigger của block nè");
            Messenger.Broadcast(EventKey.PLAYERCONNECTBLOCK);
        }
    }

    
}
