using UnityEngine;

namespace Common
{
    public static class GamePrefs
    {
        public const string PLAYER_KEY = "player";
        public const string THEME_KEY = "theme";

        public const string HIGH_SCORE_KEY = "HighScore";

        public static string GetPlayerName()
        {
            return PlayerPrefs.GetString(PLAYER_KEY, GameConfig.PLAYER_NAME);
        }
    }
}