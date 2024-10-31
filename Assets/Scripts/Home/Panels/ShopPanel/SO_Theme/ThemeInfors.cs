using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "newTheme", menuName = "ThemeInfors/Theme",order = 2)]
public class ThemeInfors : ScriptableObject
{
    [SerializeField][PreviewField(80)] 
    private Sprite backgroundSprite;
    [Header("----------Normal----------")]
    [SerializeField][PreviewField(80)] 
    private Sprite originNormalBlock;
    [SerializeField][PreviewField(80)] 
    private Sprite breakNormalBlock;

    [Header("----------Medium----------")]
    [SerializeField][PreviewField(80)] 
    private Sprite originMediumBlock;
    [SerializeField][PreviewField(80)] 
    private Sprite breakMediumBlock;

    [Header("----------Hard----------")]
    [SerializeField][PreviewField(80)] 
    private Sprite originHardBlock;
    [SerializeField][PreviewField(80)] 
    private Sprite breakHardBlock;

    [Header("----------Other----------")]
    [SerializeField][PreviewField(80)] 
    private Sprite avatarTheme;
    [SerializeField][PreviewField(80)] 
    private Sprite wallSprite;
    [SerializeField][PreviewField(80)] 
    private string nameTheme;

    public Sprite BackgroundSprite => backgroundSprite; 
    public Sprite OriginNormalBlock => originNormalBlock; 
    public Sprite BreakNormalBlock => breakNormalBlock; 
    public Sprite OriginMediumBlock => originMediumBlock; 
    public Sprite BreakMediumBlock => breakMediumBlock; 
    public Sprite OriginHardBlock => originHardBlock; 
    public Sprite BreakHardBlock => breakHardBlock; 
    public Sprite AvatarTheme => avatarTheme; 
    public Sprite WallSprite => wallSprite; 
    public string NameTheme => nameTheme; 
}
