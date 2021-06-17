using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Text>().text = Util.GetHighScore().ToString();
    }
}