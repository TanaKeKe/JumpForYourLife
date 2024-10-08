using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShopController : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    [SerializeField] private Transform content;
    [SerializeField] private Image playerSprite;
    [SerializeField] private TextMeshProUGUI namePlayer;

    private List<GameObject> _avatarPrefabs = new List<GameObject>();
    public void Start()
    {
        GenerateAvatarPlayer();
        TurnOnSpriteAndNamePlayer();
    }
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.SetOriginAvatarBtn, SetOriginBtn);
        Messenger.AddListener(EventKey.SetNewPlayerBtnToPlayerSprite, TurnOnSpriteAndNamePlayer);
        Messenger.AddListener<Image>(EventKey.SetIconShop, SetIconShop);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.SetOriginAvatarBtn, SetOriginBtn);
        Messenger.RemoveListener(EventKey.SetNewPlayerBtnToPlayerSprite, TurnOnSpriteAndNamePlayer);
        Messenger.RemoveListener<Image>(EventKey.SetIconShop, SetIconShop);
    }

    private void SetIconShop(Image image)
    {
        foreach (GameObject player in _avatarPrefabs)
        {
            PlayerInfors obj = player.GetComponent<PlayerButton>().playerInfors;
            if (PlayerPrefs.GetString("player").Equals(obj.AvatarName))
            {
                image.sprite = obj.AvatarSpriteOn;
                break;
            }
        }
    }

    private void SetOriginBtn()
    {
        foreach (GameObject player in _avatarPrefabs)
        {
            PlayerInfors obj = player.GetComponent<PlayerButton>().playerInfors;
            if (PlayerPrefs.GetString("player").Equals(obj.AvatarName))
            {
                player.GetComponent<PlayerButton>().SetOriginAvatarBtn();
                break;
            }
        }
    }

    public void TurnOnSpriteAndNamePlayer()
    {
        foreach (GameObject player in _avatarPrefabs)
        {
            PlayerInfors obj = player.GetComponent<PlayerButton>().playerInfors;

            if(PlayerPrefs.GetString("player").Equals(obj.AvatarName))
            {
                namePlayer.text = obj.AvatarName;
                playerSprite.sprite = obj.PlayerSprite;
                playerSprite.SetNativeSize();
                break;
            }
        }
    }

    private void GenerateAvatarPlayer()
    {
        PlayerInfors[] playerInfors = Resources.LoadAll<PlayerInfors>("ScriptableObjects/PlayerInfors");
        foreach (PlayerInfors player in playerInfors)
        {
            var obj = Instantiate(avatarPrefab, content);
            obj.GetComponent<PlayerButton>().Init(player);
            _avatarPrefabs.Add(obj);
        }
    }
}
