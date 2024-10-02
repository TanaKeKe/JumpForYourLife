using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class SaveGame
{
    public static void SaveHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HightScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    } 

}
