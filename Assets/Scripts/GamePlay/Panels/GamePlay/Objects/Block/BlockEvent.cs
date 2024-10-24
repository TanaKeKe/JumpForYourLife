using DG.Tweening;
using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    [SerializeField] private Sprite spriteOrigin;
    [SerializeField] private Sprite spriteBreak;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxColider2D;

    public TypeBlock typeBlock;
    public bool isBreak;

    private int _countChange;
    private bool _checkDotween;

    private void Start()
    {
        LoadSkinNormalBlock();
    }

    public void LoadSkinNormalBlock()
    {
        string nameTheme = PlayerPrefs.GetString("theme");
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + nameTheme);
        spriteRenderer.sprite = theme.OriginNormalBlock;
        spriteOrigin = theme.OriginNormalBlock;
        spriteBreak = theme.BreakNormalBlock;
    }

    public void LoadSkinMediumBlock()
    {
        string nameTheme = PlayerPrefs.GetString("theme");
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + nameTheme);
        spriteRenderer.sprite = theme.OriginMediumBlock;
        spriteOrigin = theme.OriginMediumBlock;
        spriteBreak = theme.BreakMediumBlock;
    }

    public void LoadSkinHardBlock()
    {
        string nameTheme = PlayerPrefs.GetString("theme");
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + nameTheme);
        spriteRenderer.sprite = theme.OriginHardBlock;
        spriteOrigin = theme.OriginHardBlock;
        spriteBreak = theme.BreakHardBlock;
    }

    public void SetColliderNormalBlock()
    {
        boxColider2D.size = new Vector2(1.1f, boxColider2D.size.y);
    }

    public void SetColliderMediumBlock()
    {
        boxColider2D.size = new Vector2(0.85f, boxColider2D.size.y);
    }

    public void SetColliderHardBlock()
    {
        boxColider2D.size = new Vector2(0.6f, boxColider2D.size.y);
    }

    public void SetOriginBlock()
    {
        spriteRenderer.sprite = spriteOrigin;
        spriteRenderer.color = Color.white;
        isBreak = false;
        _countChange = 0;
        boxColider2D.isTrigger = false;
    }

    public void SetHiddenBlock()
    {
        //spriteRenderer.DOFade(0, 3f).SetLoops(-1, LoopType.Yoyo);
        _checkDotween = true;
    }
    private void Update()
    {
        if(_checkDotween)
        {
            if (spriteRenderer.color.a >= 1f) spriteRenderer.DOFade(0, 2.5f);
            if (spriteRenderer.color.a <= 0f) spriteRenderer.DOFade(1, 2.5f);
        }
    }

    public void SetDoKill()
    {
        if (_checkDotween)
        {
            //spriteRenderer.DOKill();
            _checkDotween = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBreak = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            if (isBreak == true)
            {
                ++_countChange;
                if (_countChange == 1)
                {
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.BreakSound);
                    spriteRenderer.sprite = spriteBreak;
                }

                if (_countChange == 2)
                {
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.BreakSound);
                    spriteRenderer.sprite = null;
                    Messenger.Broadcast(EventKey.SetNullParentOfPlayer);
                    Messenger.Broadcast(EventKey.SetStatusBlockWhenPlay);
                }
            }
        }
    }
}
