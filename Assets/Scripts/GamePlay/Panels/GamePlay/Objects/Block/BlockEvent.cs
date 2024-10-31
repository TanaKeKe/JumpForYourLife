using Common;
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
    private float _timeFaded;
    private float _minValueFaded;
    private float _maxValueFaded;
    private float _sizeXOfColliderNormalBlock;
    private float _sizeXOfColliderMediumBlock;
    private float _sizeXOfColliderHardBlock;

    private void Start()
    {
        _timeFaded = 2.5f;
        _minValueFaded = 0f;
        _maxValueFaded = 1f;

        _sizeXOfColliderNormalBlock = 1.1f;
        _sizeXOfColliderMediumBlock = 0.85f;
        _sizeXOfColliderHardBlock = 0.6f;
        LoadSkinNormalBlock();
    }

    public void LoadSkinNormalBlock()
    {
        string nameTheme = GamePrefs.GetThemeOriginName();
        ThemeInfors theme = Resources.Load<ThemeInfors>(GameConfig.THEME_INFOR_PATH + "/" + nameTheme);
        spriteRenderer.sprite = theme.OriginNormalBlock;
        spriteOrigin = theme.OriginNormalBlock;
        spriteBreak = theme.BreakNormalBlock;
    }

    public void LoadSkinMediumBlock()
    {
        string nameTheme = GamePrefs.GetThemeOriginName();
        ThemeInfors theme = Resources.Load<ThemeInfors>(GameConfig.THEME_INFOR_PATH + "/" + nameTheme);
        spriteRenderer.sprite = theme.OriginMediumBlock;
        spriteOrigin = theme.OriginMediumBlock;
        spriteBreak = theme.BreakMediumBlock;
    }

    public void LoadSkinHardBlock()
    {
        string nameTheme = GamePrefs.GetThemeOriginName();
        ThemeInfors theme = Resources.Load<ThemeInfors>(GameConfig.THEME_INFOR_PATH + "/" + nameTheme);
        spriteRenderer.sprite = theme.OriginHardBlock;
        spriteOrigin = theme.OriginHardBlock;
        spriteBreak = theme.BreakHardBlock;
    }

    public void SetColliderNormalBlock()
    {
        boxColider2D.size = new Vector2(_sizeXOfColliderNormalBlock, boxColider2D.size.y);
    }

    public void SetColliderMediumBlock()
    {
        boxColider2D.size = new Vector2(_sizeXOfColliderMediumBlock, boxColider2D.size.y);
    }

    public void SetColliderHardBlock()
    {
        boxColider2D.size = new Vector2(_sizeXOfColliderHardBlock, boxColider2D.size.y);
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
            if (spriteRenderer.color.a >= _maxValueFaded) spriteRenderer.DOFade(_minValueFaded, _timeFaded);
            if (spriteRenderer.color.a <= _minValueFaded) spriteRenderer.DOFade(_maxValueFaded, _timeFaded);
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
        if (collision.gameObject.CompareTag(GameTags.PLAYER_TAG))
        {
            isBreak = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GameTags.WALL_TAG))
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
