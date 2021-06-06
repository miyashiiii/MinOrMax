using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    public GameObject debugJudgeText;

    void Start()
    {
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);
        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();
        GameManager.AddFinishListener(Onfinish);
        GameManager.AddButtonClickListener(OnButtonClick);
    }

    private void Onfinish()
    {
        debugJudgeText.GetComponent<Text>().text = "GameOver";
    }

    private void Update()
    {
        var remainTimeStr = GameManager.remainTime <= 0 ? "0" : GameManager.remainTime.ToString();

        timeText.GetComponent<Text>().text = remainTimeStr;
        comboText.GetComponent<Text>().text = GameManager.combo.ToString();
        scoreText.GetComponent<Text>().text = GameManager.score.ToString();
    }


    public void OnButtonClick(bool result, bool isQuick)
    {
        var resultStr = result ? (isQuick ? "good" : "ok") : "bad";

        debugJudgeText.GetComponent<Text>().text = resultStr;


        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);

        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();
    }
}