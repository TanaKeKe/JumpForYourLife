using Common;
using UnityEngine;
using UnityEngine.UI;

public class ThemeButton : MonoBehaviour
{
    [SerializeField] private GameObject tickSelection;
    [SerializeField] private Image themeBtn;

    public ThemeInfors themeInfors;
    private Color _originColorSelection;

    public void Init(ThemeInfors themeInfors)
    {
        this.themeInfors = themeInfors;
        themeBtn.sprite = this.themeInfors.AvatarTheme;
        if (PlayerPrefs.GetString(GamePrefs.THEME_KEY).Equals(this.themeInfors.NameTheme))
        {
            SetBtnSelected();
        }
    }

    public void OnClick()
    {
        Messenger.Broadcast(EventKey.SetOriginThemeBtn);
        SetBtnSelected();
        PlayerPrefs.SetString(GamePrefs.THEME_KEY, themeInfors.NameTheme);
        Messenger.Broadcast(EventKey.SetNewThemeBtnToThemeSprite);
    }

    private void SetBtnSelected()
    {
        tickSelection.SetActive(true);
        _originColorSelection = themeBtn.color;
        themeBtn.color = Color.white;
    }

    public void SetOriginThemeBtn()
    {
        tickSelection.SetActive(false);
        themeBtn.color = _originColorSelection;
    }
}
