using UnityEngine;

public class Wall : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Vector2 _wallHeight;

    private void Start()
    {
        _wallHeight = new Vector2(0, 10f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.MoveWall, MoveWall);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.MoveWall, MoveWall);
    }

    private void MoveWall()
    {
        _spriteRenderer.size += _wallHeight;
        _collider.size = _spriteRenderer.size;
    }
}
