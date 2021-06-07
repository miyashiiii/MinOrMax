using System.Collections;
using System.Collections.Generic;
using Shapes2D;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    public GameObject timeText;
    public GameObject comboText;
    public GameObject scoreText;
    private int time;
    private int combo;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        time=(int)(GameManager.endTime-GameManager.startTime);
        var mm = (time / 60).ToString("00");
        var ss = (time % 60).ToString("00");
        timeText.GetComponent<Text>().text = mm + ":" + ss; 
        combo = GameManager.maxCombo;
        score = GameManager.score;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.GetComponent<Text>().text = time.ToString();
        comboText.GetComponent<Text>().text = combo.ToString();
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
