using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Vector2 _wallHeight = new Vector2(0, 10f);

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.MoveWall, MoveWall);
    }

    private void MoveWall()
    {
        _spriteRenderer.size += _wallHeight;
        _collider.size = _spriteRenderer.size;
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.MoveWall, MoveWall);
    }
}
