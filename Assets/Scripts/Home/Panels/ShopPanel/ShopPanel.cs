using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : Panel
{
    [Header("----------Image And Color----------")]
    [SerializeField] private Image playerBtnImage;
    [SerializeField] private Image themeBtnImage;
    [SerializeField] private GameObject iconSelectOfPlayer;
    [SerializeField] private GameObject iconSelectOfTheme;

    [Header("----------Selection Player----------")]
    [SerializeField] private Image playerSprite;
    [SerializeField] private TextMeshProUGUI namePlayer;

    [Header("----------Shop----------")]
    [SerializeField] private GameObject playerShop;
    [SerializeField] private GameObject themeShop;
    private Color _selectColor;
    private void Start()
    {
        _selectColor = playerBtnImage.color;
    }
    public void ClosePanel()
    {
        PanelManager.Instance.ClosePanel("ShopPanel");
    }

    public void OpenIconSelectOfPlayer()
    {
        themeBtnImage.color = Color.white;
        playerBtnImage.color = _selectColor;
        iconSelectOfPlayer.SetActive(true);
        iconSelectOfTheme.SetActive(false);

        playerShop.SetActive(true);
        themeShop.SetActive(false);
    }

    public void OpenIconSelectOfTheme()
    {
        themeBtnImage.color = _selectColor;
        playerBtnImage.color = Color.white;
        iconSelectOfPlayer.SetActive(false);
        iconSelectOfTheme.SetActive(true);

        playerShop.SetActive(false);
        themeShop.SetActive(true);
    }
}
