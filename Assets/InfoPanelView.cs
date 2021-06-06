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
    public GameObject gameOverText;

    void Start()
    {
        gameOverText.GetComponent<Text>().text = "";
 
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);
        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();
        GameManager.AddFinishListener(Onfinish);
        GameManager.AddButtonClickListener(OnButtonClick);
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }
    private void Onfinish()
    {
        gameOverText.GetComponent<Text>().text = "GameOver";
        debugCondText.GetComponent<Text>().text = "-";

        debugCondNumText.GetComponent<Text>().text = "-";
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }

    private void Update()
    {
        if (!GameManager.isFinish())
        {
            gameOverText.GetComponent<Text>().text = "";
 
        }
        var remainTimeStr = GameManager.remainTime <= 0 ? "0.0" : GameManager.remainTime.ToString("0.0");

        timeText.GetComponent<Text>().text = remainTimeStr;
        comboText.GetComponent<Text>().text = GameManager.combo.ToString();
        scoreText.GetComponent<Text>().text = GameManager.score.ToString();
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);

        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();
        if (CurrentjudgeTextAnimationFrames > 0)
        {
            CurrentjudgeTextAnimationFrames--;
        }
        judgeText.GetComponent<Text>().color =new Color(0,0,0,(float)CurrentjudgeTextAnimationFrames/judgeTextAnimationFrames);
        
    }

    
    string quickCorrectStr = "Great!   +" + GameManager.quickCorrectAddTime + "sec";
    string correctStr = "Good   +" + GameManager.correctAddTime + "sec";
    string missStr = "miss...   -" + GameManager.missAddTime + "sec";
    const int judgeTextAnimationFrames=50;
    int CurrentjudgeTextAnimationFrames=0;
    
    public void OnButtonClick(bool result, bool isQuick)
    {
        var resultStr = result ? (isQuick ? quickCorrectStr : correctStr) : missStr;

        judgeText.GetComponent<Text>().text = resultStr;
        CurrentjudgeTextAnimationFrames = judgeTextAnimationFrames;

    }
}