using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeShopController : MonoBehaviour
{
    [SerializeField] private GameObject themePrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Image themeSprite;
    [SerializeField] private TextMeshProUGUI nameTheme;

    private List<GameObject> _themePrefabs = new List<GameObject>();

    public void Start()
    {
        GenerateTheme();
        TurnOnSpriteAndNameTheme();
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetOriginThemeBtn, SetOriginBtn);
        Messenger.AddListener(EventKey.SetNewThemeBtnToThemeSprite, TurnOnSpriteAndNameTheme);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetOriginThemeBtn, SetOriginBtn);
        Messenger.RemoveListener(EventKey.SetNewThemeBtnToThemeSprite, TurnOnSpriteAndNameTheme);
    }

    private void SetOriginBtn()
    {
        foreach (GameObject theme in _themePrefabs)
        {
            ThemeInfors obj = theme.GetComponent<ThemeButton>().themeInfors;
            if (PlayerPrefs.GetString("theme").Equals(obj.NameTheme))
            {
                theme.GetComponent<ThemeButton>().SetOriginThemeBtn();
                break;
            }
        }
    }

    private void TurnOnSpriteAndNameTheme()
    {
        foreach (GameObject theme in _themePrefabs)
        {
            ThemeInfors obj = theme.GetComponent<ThemeButton>().themeInfors;

            if (PlayerPrefs.GetString("theme").Equals(obj.NameTheme))
            {
                nameTheme.text = obj.NameTheme;
                themeSprite.sprite = obj.BackgroundSprite;
                break;
            }
        }
    }

    private void GenerateTheme()
    {
        ThemeInfors[] themeInfors = Resources.LoadAll<ThemeInfors>("ScriptableObjects/ThemeInfors");
        foreach (ThemeInfors theme in themeInfors)
        {
            var obj = Instantiate(themePrefab, content);
            obj.GetComponent<ThemeButton>().Init(theme);
            _themePrefabs.Add(obj);
        }
    }
}
