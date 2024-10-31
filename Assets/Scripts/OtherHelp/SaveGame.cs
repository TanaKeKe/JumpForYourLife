using Common;
using TMPro;
using UnityEngine;

public static class SaveGame
{
    public static void SaveHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt(GamePrefs.HIGH_SCORE_KEY,0))
        {
            PlayerPrefs.SetInt(GamePrefs.HIGH_SCORE_KEY, score);
            PlayerPrefs.Save();
        }
    } 
}
