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
    public GameObject newRecordText;
    

    // Start is called before the first frame update
    void Start()
    {
        var time=(int)(GameManager.endTime-GameManager.startTime)-2;
        var mm = (time / 60).ToString("00");
        var ss = (time % 60).ToString("00");
        timeText.GetComponent<Text>().text = mm + ":" + ss; 
        var combo = GameManager.maxCombo;
        var score = GameManager.score;
        comboText.GetComponent<Text>().text = combo.ToString();
        scoreText.GetComponent<Text>().text = score.ToString();
        var isNewRecord = GameManager.isNewRecord;
        newRecordText.SetActive(isNewRecord);
            
    }

    // Update is called once per frame
    void Update()
    {
    }
}
