using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newPlayerInfors", menuName = "PlayerInfors/Player", order = 1)]
public class PlayerInfors : ScriptableObject
{
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite avatarSpriteOn;
    [SerializeField] private Sprite avatarSpriteOff;
    [SerializeField] private string avatarName;

    public Sprite PlayerSprite { get { return playerSprite; } }
    public Sprite AvatarSpriteOn { get { return avatarSpriteOn; } }
    public Sprite AvatarSpriteOff { get { return avatarSpriteOff; } }
    public string AvatarName { get { return avatarName; } }
}
