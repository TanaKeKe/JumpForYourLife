using UnityEngine;

[CreateAssetMenu(fileName = "newTheme", menuName = "ThemeInfors/Theme",order = 2)]
public class ThemeInfors : ScriptableObject
{
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite originLengthBlock;
    [SerializeField] private Sprite originShortBlock;
    [SerializeField] private Sprite breakLengthBlock;
    [SerializeField] private Sprite breakShortBlock;
    [SerializeField] private Sprite avatarTheme;
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private string nameTheme;

    public Sprite BackgroundSprite { get { return backgroundSprite; } }
    public Sprite OriginLengthBlock { get { return originLengthBlock; } }
    public Sprite BreakLengthBlock { get { return breakLengthBlock; } }
    public Sprite OriginShortBlock { get { return originShortBlock; } }
    public Sprite BreakShortBlock { get { return breakShortBlock; } }
    public Sprite AvatarTheme { get { return avatarTheme; } }
    public Sprite WallSprite { get { return wallSprite; } }
    public string NameTheme { get { return nameTheme; } }

    private void OnValidate()
    {
        nameTheme = name;
    }

}
