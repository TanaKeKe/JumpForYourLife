using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockEvent : MonoBehaviour
{
    [SerializeField] private Sprite spriteOrigin;
    [SerializeField] private Sprite spriteBreak;
    [Space(10)]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Collider2D col2D;
    public bool isBreak;
    public int countChange;

    private void Start()
    {
        LoadSkinBlock();
    }

    private void LoadSkinBlock()
    {
        string nameTheme = PlayerPrefs.GetString("theme");
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + nameTheme);
        spriteRenderer.sprite = theme.OriginLengthBlock;
        spriteOrigin = theme.OriginLengthBlock;
        spriteBreak = theme.BreakLengthBlock;
    }

    private void OnEnable()
    {
        Messenger.AddListener<GameObject>(EventKey.SetOriginBlock, SetOriginBlock);
    }
    private void OnDisable()
    {
        Messenger.RemoveListener<GameObject>(EventKey.SetOriginBlock, SetOriginBlock);
    }

    private void SetOriginBlock(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOrigin;
        gameObject.GetComponent<BlockEvent>().isBreak = false;
        gameObject.GetComponent<BlockEvent>().countChange = 0;
        gameObject.GetComponent<Collider2D>().isTrigger = false;
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
