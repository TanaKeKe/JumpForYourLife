using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawn : MonoBehaviour
{
    [SerializeField] private GameObject walls;
    [SerializeField] private GameObject wallTinyRightPrefab;
    [SerializeField] private Transform wallRight;
    [SerializeField] private GameObject wallTinyLeftPrefab;
    [SerializeField] private Transform wallLeft;
    [Space(10)]

    [SerializeField] private int wallCount;

    private GameObject _wallTinyLeftClone;
    private SpriteRenderer _wallTinyLeftSpriteRenderer;
    private GameObject _wallTinyRightClone;
    private SpriteRenderer _wallTinyRightSpriteRenderer;
    private Vector2 _wallHeight = new Vector2(0, 10f);
    private void Start()
    {
        LoadSkinWall();
        wallTinyRightPrefab.GetComponent<SpriteRenderer>().sprite = wallTinyLeftPrefab.GetComponent<SpriteRenderer>().sprite;
        GenerateWall(wallTinyRightPrefab, wallTinyLeftPrefab);
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
        _wallTinyLeftSpriteRenderer.size += _wallHeight;
        _wallTinyRightSpriteRenderer.size += _wallHeight;
    }

    private void LoadSkinWall()
    {
        ThemeInfors theme = Resources.Load<ThemeInfors>("ScriptableObjects/ThemeInfors/" + PlayerPrefs.GetString("theme"));
        wallTinyLeftPrefab.GetComponent<SpriteRenderer>().sprite = theme.WallSprite;
    }

    private void GenerateWall(GameObject wallTinyRight, GameObject wallTinyLeft)
    {
        for (int i = 1; i <= wallCount; ++i)
        {
            _wallTinyLeftClone = Instantiate(wallTinyLeft, wallLeft);
            _wallTinyLeftSpriteRenderer = _wallTinyLeftClone.GetComponent<SpriteRenderer>();
            _wallTinyRightClone = Instantiate(wallTinyRight, wallRight);
            _wallTinyRightSpriteRenderer = _wallTinyRightClone.GetComponent<SpriteRenderer>();
        }
    }
}
