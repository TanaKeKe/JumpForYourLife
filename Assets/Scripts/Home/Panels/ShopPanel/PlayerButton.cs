using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButton : MonoBehaviour
{
    [SerializeField] private GameObject iconOnGameObject;
    [SerializeField] private GameObject iconOffGameObject;
    [SerializeField] private Image avatarBtn;

    private Color _halfWhite = new Color(1, 1, 1, .5f);
    public PlayerInfors playerInfors;
    public void Init(PlayerInfors playerInfors)
    {
        this.playerInfors = playerInfors;
        iconOnGameObject.GetComponent<Image>().sprite = this.playerInfors.AvatarSpriteOn;
        iconOnGameObject.GetComponent<Image>().SetNativeSize();

        iconOffGameObject.GetComponent<Image>().sprite = this.playerInfors.AvatarSpriteOff;
        iconOffGameObject.GetComponent<Image>().SetNativeSize();

        SetButtonState();
    }

    private void SetButtonState()
    {
        if (PlayerPrefs.GetString("player").Equals(this.playerInfors.AvatarName))
        {
            SetBtnSelected();
        }
        else
        {
            SetOriginAvatarBtn();
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetOriginAvatarBtn,SetButtonState);   
    }
    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetOriginAvatarBtn, SetButtonState);
    }


    public void OnClick()
    {
        PlayerPrefs.SetString("player", playerInfors.AvatarName);
        Messenger.Broadcast(EventKey.SetOriginAvatarBtn);
        SetBtnSelected();
        Messenger.Broadcast(EventKey.SetNewPlayerBtnToPlayerSprite);
    }

    private void SetBtnSelected()
    {
        iconOffGameObject.SetActive(false);
        iconOnGameObject.SetActive(true);
        avatarBtn.color = Color.white;
    }
    public void SetOriginAvatarBtn()
    {
        iconOffGameObject.SetActive(true);
        iconOnGameObject.SetActive(false);
        avatarBtn.color = _halfWhite;
    }
}
