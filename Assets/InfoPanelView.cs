using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = System.Random;

public class InfoPanelView : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject comboText;
    public GameObject timeText;

    public GameObject debugCondText;
    public GameObject debugCondNumText;
    public GameObject judgeText;
    public GameObject timeUpText;

    void Start()
    {
        timeUpText.GetComponent<Text>().text = "";

        UpdateCondText();
        GameManager.AddFinishListener(Onfinish);
        GameManager.AddButtonClickListener(OnButtonClick);
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }

    private void UpdateCondText()
    {
        Color color;
    string condStr;
        if (GameManager.cond == GameManager.Condition.Max)
        {
            condStr = "▲ Max";
            color = new Color(1, 0.4f, 0.4f);
        }
        else
        {
            condStr = "▼ Min";
            color = new Color(0.3f, 0.7f, 1);
            
        };
        debugCondText.GetComponentInChildren<Text>().text = condStr;
        debugCondText.GetComponent<Image>().color = color;
    }
    private void Onfinish()
    {
        timeUpText.GetComponent<Text>().text = "Time up";
        UpdateCondText();
 
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }

    private void Update()
    {
    UpdateCondText();
        if (!GameManager.isFinish())
        {
            timeUpText.GetComponent<Text>().text = "";
 
        }
        var remainTimeStr = GameManager.remainTime <= 0 ? "0.0" : GameManager.remainTime.ToString("0.0");

        timeText.GetComponent<Text>().text = remainTimeStr;
        comboText.GetComponent<Text>().text = GameManager.combo.ToString();
        scoreText.GetComponent<Text>().text = GameManager.score.ToString();
        

        if (CurrentjudgeTextAnimationFrames > 0)
        {
            CurrentjudgeTextAnimationFrames--;
        }
        judgeText.GetComponent<Text>().color =new Color(0,0,0,(float)CurrentjudgeTextAnimationFrames/judgeTextAnimationFrames);
        
    }

    
    string quickCorrectStr = "Great!   +" + GameManager.quickCorrectAddTime + "sec";
    string correctStr = "Good   +" + GameManager.correctAddTime + "sec";
    string missStr = "miss...   " + GameManager.missAddTime + "sec";
    const int judgeTextAnimationFrames=50;
    int CurrentjudgeTextAnimationFrames=0;
    
    public void OnButtonClick(bool result, bool isQuick)
    {
        var resultStr = result ? (isQuick ? quickCorrectStr : correctStr) : missStr;

        judgeText.GetComponent<Text>().text = resultStr;
        CurrentjudgeTextAnimationFrames = judgeTextAnimationFrames;

    }
}