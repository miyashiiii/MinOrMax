using UnityEngine;

public static class Util
{
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HIGH_SCORE");
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HIGH_SCORE", score);
        PlayerPrefs.Save();
    }
}