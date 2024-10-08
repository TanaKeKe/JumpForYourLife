using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private GameObject iconOnGameObject;
    [SerializeField] private GameObject iconOffGameObject;
    [SerializeField] private GameObject tickSelection;
    [SerializeField] private Image avatarBtn;

    public PlayerInfors playerInfors;
    private Color _originColorSelection;
    public void Init(PlayerInfors playerInfors)
    {
        this.playerInfors = playerInfors;
        iconOnGameObject.GetComponent<Image>().sprite = this.playerInfors.AvatarSpriteOn;
        iconOnGameObject.GetComponent<Image>().SetNativeSize();

        iconOffGameObject.GetComponent<Image>().sprite = this.playerInfors.AvatarSpriteOff;
        iconOffGameObject.GetComponent<Image>().SetNativeSize();

        if (PlayerPrefs.GetString("player").Equals(this.playerInfors.AvatarName))
        {
            SetBtnSelected();
        }
    }

    public void OnClick()
    {
        Messenger.Broadcast(EventKey.SetOriginAvatarBtn);
        SetBtnSelected();
        PlayerPrefs.SetString("player", playerInfors.AvatarName);
        Messenger.Broadcast(EventKey.SetNewPlayerBtnToPlayerSprite);
    }

    private void SetBtnSelected()
    {
        iconOffGameObject.SetActive(false);
        iconOnGameObject.SetActive(true);
        tickSelection.SetActive(true);
        _originColorSelection = avatarBtn.color;
        avatarBtn.color = Color.white;
    }

    public void SetOriginAvatarBtn()
    {
        iconOffGameObject.SetActive(true);
        iconOnGameObject.SetActive(false);
        tickSelection.SetActive(false);
        avatarBtn.color = _originColorSelection;
    }
}
