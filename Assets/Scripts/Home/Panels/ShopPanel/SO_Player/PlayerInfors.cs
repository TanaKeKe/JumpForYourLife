using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerInfors", menuName = "PlayerInfors/Player", order = 1)]
public class PlayerInfors : ScriptableObject
{
    [SerializeField] [PreviewField(80)]
    private Sprite playerSprite;

    [SerializeField] [PreviewField(80)]
    private Sprite avatarSpriteOn;

    [SerializeField] [PreviewField(80)]
    private Sprite avatarSpriteOff;
    
    [SerializeField] private string avatarName;

    public Sprite PlayerSprite => playerSprite;
    public Sprite AvatarSpriteOn => avatarSpriteOn;
    public Sprite AvatarSpriteOff => avatarSpriteOff;
    public string AvatarName => avatarName;
}