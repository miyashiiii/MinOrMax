using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("HIGH_SCORE").ToString();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}