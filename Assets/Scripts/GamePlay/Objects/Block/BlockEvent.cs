using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockEvent : MonoBehaviour
{
    [SerializeField] private Sprite spriteOrigin;
    [SerializeField] private Sprite spriteBreak;
    [Space(10)]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D col2D;

    public bool isBreak;
    public int countChange;
    public TypeBlock typeBlock;
    public int endIndex = 0;
    public static BlockEvent Instance;
    private bool checkDotween;

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
        col2D.size = new Vector2(1.1f, col2D.size.y);
    }

    public void SetColliderMediumBlock()
    {
        col2D.size = new Vector2(0.85f, col2D.size.y);
    }

    public void SetColliderHardBlock()
    {
        col2D.size = new Vector2(0.6f, col2D.size.y);
    }

    public void SetOriginBlock()
    {
        spriteRenderer.sprite = spriteOrigin;
        isBreak = false;
        countChange = 0;
        col2D.isTrigger = false;
    }

    public void SetHiddenBlock()
    {
        //spriteRenderer.DOFade(0, 3f).SetLoops(-1, LoopType.Yoyo);
        checkDotween = true;
    }
    private void Update()
    {
        if(checkDotween)
        {
            if(spriteRenderer.color.a <= 0.001f)
            {
                StartCoroutine(CoroutineFade());
            }
            if(spriteRenderer.color.a >= 0.999f)
            {
                StartCoroutine(CoroutineMakeOpacity());
            }
        }
    }

    private IEnumerator CoroutineMakeOpacity()
    {
        float faded = 1f;
        while (faded > 0)
        {
            faded -= 0.001f;
            spriteRenderer.color = new Color(1f, 1f, 1f, faded);
            yield return null;
        }
    }

    private IEnumerator CoroutineFade()
    {
        float faded = 0;
        while(faded < 1f)
        {
            faded += 0.001f;
            spriteRenderer.color = new Color(1f, 1f, 1f,faded);
            yield return null;
        }
    }

    public void SetDoKill()
    {
        if (checkDotween)
        {
            //spriteRenderer.DOKill();
            spriteRenderer.color = Color.white;
            checkDotween = false;
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
                ++countChange;
                if (countChange == 1)
                {
                    AudioGamePlayManager.Instance.PlaySound(AudioGamePlayManager.Instance.BreakSound);
                    spriteRenderer.sprite = spriteBreak;
                }

                if (countChange == 2)
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
