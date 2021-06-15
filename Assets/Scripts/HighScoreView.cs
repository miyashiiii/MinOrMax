using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("HIGH_SCORE").ToString();
    }
}