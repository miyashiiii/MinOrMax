using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Text>().text = PlayerPrefs.GetInt("HIGH_SCORE").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
