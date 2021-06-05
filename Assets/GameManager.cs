using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager:MonoBehaviour
{
    private static int pressedScore;
    public static int[] buttons;
    
    public static int score = 0;
    public static int combo = 0;
    public static float startTime;

    public  GameObject scoreTextTmp;
    public  GameObject highScoreTextTmp;
    public  GameObject comboTextTmp;
    public  GameObject timeTextTmp;
   
    
    // public  GameObject debugButtonNumTextTmp;
    public  GameObject debugCondTextTmp;
    public  GameObject debugCondNumTextTmp;
    public  GameObject debugJudgeTextTmp;

    public  static GameObject scoreText;
    public  static GameObject highScoreText;
    public  static GameObject comboText;
    public  static GameObject timeText;
    
    // public  static GameObject debugButtonNumText;
    public  static GameObject debugCondText;
    public  static GameObject debugCondNumText;
    public  static GameObject debugJudgeText;

    public static IEnumerable<int> baseArray = Enumerable.Range(1, 41);
    public static GameObject[] buttonObjects = new GameObject[16];
 
    public enum Condition
    {
        Min,
        Max
    }

    public static Condition cond = Condition.Min;
    public static int condNum = 1;

    public static float lastTime;
    public static float quickTh=1f;
    public static float baseSuccessScore = 10f;
    
    public static int initTime=60;
    public static int remainTime;
    void Start()
    {
        scoreText=scoreTextTmp;
        highScoreText=highScoreTextTmp;
        comboText=comboTextTmp;
        timeText=timeTextTmp;
    
        // debugButtonNumText=debugButtonNumTextTmp;
        debugCondText=debugCondTextTmp;
        debugCondNumText=debugCondNumTextTmp;
        debugJudgeText=debugJudgeTextTmp; 
        
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);
        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();

        int[]randNumArray=GenRandNumArray();
        GameManager.buttons = randNumArray;
        for (int i = 0; i < 16; i++)
        {
            var button = transform.GetChild(i).gameObject;
            buttonObjects[i] = button;
            buttonObjects[i].transform.GetComponentInChildren<Text>().text =randNumArray[i].ToString(); 
        }

        startTime = Time.time;
        remainTime = initTime;
    }

    private static int addTime = 0;
    private void Update()
    {
        var spend=(int)(Time.time - startTime);
        remainTime = initTime-spend+addTime;
        timeText.GetComponent<Text>().text = remainTime.ToString();
        if (remainTime == 0)
        {
            Finish();
        }

    }

    private void Finish()
    {
        
    }
    
    int[] GenRandNumArray()
    {
 
        var ary = baseArray.OrderBy(n => Guid.NewGuid()).Take(16).ToArray();
        return ary;
    }

    public static int CalcScore(int combo, bool isQuick)
    {
        var score = baseSuccessScore;

        if (isQuick)
        {
            score *= 1.5f;
        }
        if (combo < 5)
        {
            score *= 1f;
        }else if (combo < 10)
        {
            score *= 1.2f;
            
        }
        else if (combo < 20)
        {
            score *= 1.5f;
            
        }
        else if (combo < 30)
        {
            score *= 1.8f;
            
        }
        else
        {
            score *= 2f;
            
        }

        return (int)score;
    }
    public static void OnButtonClick(int num)
    {
        // debugButtonNumText.GetComponent<Text>().text = num.ToString();
        var result=judge(num);
        string resultStr;
        var isQuick = (Time.time-lastTime)<quickTh;
        if (result)
        {
            if (isQuick)
            {
            resultStr = "good";
            }
            else
            {
            resultStr = "ok";
                
            }
            combo++;
            score += CalcScore(combo, isQuick);
            addTime += 1;
        }
        else
        {
            resultStr = "bad";
            combo=0;
            addTime -= 5;
        }
        debugJudgeText.GetComponent<Text>().text = resultStr;
        comboText.GetComponent<Text>().text = combo.ToString();
        scoreText.GetComponent<Text>().text = score.ToString();
 
        // ボタン置き換え
        var bList = new List<int>(buttons);
 
        var btnIdx=bList.IndexOf(num);
        var notInButtonsList = baseArray.Except(buttons).ToArray();
        var newValue=  notInButtonsList[ UnityEngine.Random.Range(0, notInButtonsList.Length) ];
        buttons[btnIdx] = newValue;
        buttonObjects[btnIdx].transform.GetComponentInChildren<Text>().text =newValue.ToString(); 
         
        Debug.Log("new value:"+newValue);
        Debug.Log("btnIdx:"+btnIdx);
        
        // 条件リセット
        RandomCondition();
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(Condition), cond);

        debugCondNumText.GetComponent<Text>().text = condNum.ToString();
        lastTime = Time.time;
    }

    public static bool judge(int num)
    {
        var bList = new List<int>(buttons);
        switch (cond)
        {
            case Condition.Max when bList.Max() == num:
            case Condition.Min when bList.Min() == num:
                return true;
            default:
                return false;
        }
    }

    public static void RandomCondition()
    {
        Array values = Enum.GetValues(typeof(Condition));
        Random random1 = new Random();
        cond = (Condition) values.GetValue(random1.Next(values.Length));
        Random random2 = new Random();
        condNum = random2.Next(1, 2); //TODO
        // condNum=random2.Next(1, 3);
    }
}