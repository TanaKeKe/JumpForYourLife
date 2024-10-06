using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    [SerializeField] private Sprite spriteOrigin;
    [SerializeField] private Sprite spriteBreak;
    [Space(10)]

    private SpriteRenderer _spriteRenderer;
    public bool _isBreak;
    public int _countChange;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Messenger.AddListener<GameObject>(EventKey.SetOriginSprite, SetOriginBlock);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<GameObject>(EventKey.SetOriginSprite, SetOriginBlock);
    }

    private void SetOriginBlock(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOrigin;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isBreak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isBreak = false;
            _countChange = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (_isBreak)
            {
                ++_countChange;
                if (_countChange == 1)
                {
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.BreakSound);
                    _spriteRenderer.sprite = spriteBreak;
                }

                if (_countChange == 2)
                {
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.BreakSound);
                    _spriteRenderer.sprite = null;
                    Messenger.Broadcast(EventKey.SetNullParentOfPlayer);
                    Messenger.Broadcast(EventKey.PlayerOnTriggerBlock);
                }
            }
        }
    }

    private void SetOriginSprite()
    {
        _spriteRenderer.sprite = spriteOrigin;
    }
}
