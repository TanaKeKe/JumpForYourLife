using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
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
    private void Start()
    {
        LoadSkinWall();
        wallTinyRightPrefab.GetComponent<SpriteRenderer>().sprite = wallTinyLeftPrefab.GetComponent<SpriteRenderer>().sprite;
        GenerateWall(wallTinyRightPrefab, wallTinyLeftPrefab);
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
            Instantiate(wallTinyLeft, wallLeft);
            Instantiate(wallTinyRight, wallRight);
        }
    }
}
