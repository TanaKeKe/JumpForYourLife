using System.Collections;
using Common;
using DG.Tweening;
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
    [SerializeField] private GameObject popup;
    [SerializeField] private Image image;

    [Header("----------Private----------")]
    private Color _selectColor;

    private void Start()
    {
        popup.transform.DOScale(1, 0.6f).SetEase(Ease.OutBack);
        _selectColor = playerBtnImage.color;
    }

    public void ClosePanel()
    {
        popup.transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
        image.color = new Color(1, 1, 1, 0);
        StartCoroutine(CouroutinePopup());
    }

    private IEnumerator CouroutinePopup()
    {
        yield return new WaitForSeconds(0.5f);
        PanelManager.Instance.ClosePanel(GameConfig.SHOP_PANEL);
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
