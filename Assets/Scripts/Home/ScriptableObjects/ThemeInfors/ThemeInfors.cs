using UnityEngine;

[CreateAssetMenu(fileName = "newTheme", menuName = "ThemeInfors/Theme",order = 2)]
public class ThemeInfors : ScriptableObject
{
    [SerializeField] private Sprite backgroundSprite;
    [Header("----------Normal----------")]
    [SerializeField] private Sprite originNormalBlock;
    [SerializeField] private Sprite breakNormalBlock;

    [Header("----------Medium----------")]
    [SerializeField] private Sprite originMediumBlock;
    [SerializeField] private Sprite breakMediumBlock;

    [Header("----------Other----------")]
    [SerializeField] private Sprite avatarTheme;
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private string nameTheme;

    public Sprite BackgroundSprite { get { return backgroundSprite; } }
    public Sprite OriginNormalBlock { get { return originNormalBlock; } }
    public Sprite BreakNormalBlock { get { return breakNormalBlock; } }
    public Sprite OriginMediumBlock { get { return originMediumBlock; } }
    public Sprite BreakMediumBlock { get { return breakMediumBlock; } }
    public Sprite AvatarTheme { get { return avatarTheme; } }
    public Sprite WallSprite { get { return wallSprite; } }
    public string NameTheme { get { return nameTheme; } }

    private void OnValidate()
    {
        nameTheme = name;
    }

}
