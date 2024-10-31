using UnityEngine;

namespace Common
{
    public static class GamePrefs
    {
        public const string PLAYER_KEY = "player";
        public const string THEME_KEY = "theme";

        public const string HIGH_SCORE_KEY = "HighScore";

        // sound
        public const string SOUND_KEY = "SoundOn";
        public const string SOUND_VOLUME_KEY = "SoundVolume";
        public const string MUSIC_KEY = "MusicOn";
        public const string MUSIC_VOLUME_KEY = "MusicVolume";

        public static string GetPlayerOriginName()
        {
            return PlayerPrefs.GetString(PLAYER_KEY, GameConfig.PLAYER_NAME);
        }

        public static string GetThemeOriginName()
        {
            return PlayerPrefs.GetString(THEME_KEY, GameConfig.THEME_NAME);
        }
    }
}