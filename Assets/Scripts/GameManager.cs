using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private static int pressedScore;
    public static int[] buttons;

    public static int score = 0;
    public static int combo = 0;
    public static int maxCombo = 0;
    public static float startTime;
    public static float endTime;

    public const int maxNum = 30;
    public static IEnumerable<int> baseArray = Enumerable.Range(1, maxNum);
    public static GameObject[] buttonObjects = new GameObject[16];

    public enum Condition
    {
        Min,
        Max
    }

    public static Condition cond = Condition.Min;
    public static int condNum = 1;

    public static float lastTime;
    public static float quickTh = 1f;
    public static float baseSuccessScore = 10f;

    public static float gameTime = 20;
    public static float remainTime;
    static int boardSideSize = 2;
    static int boardSize = boardSideSize * boardSideSize;

    public enum Status
    {
        InGame,
        Finish
    }

    public static Status status = Status.InGame;
    static Transform buttonsTransform;

    void Start()
    {
        buttonsTransform = transform;


    }

    public static void InitGame()
    {
        buttons = GenRandNumArray();
        for (int i = 0; i < boardSize; i++)
        {
            var button = buttonsTransform.GetChild(i).gameObject;
            buttonObjects[i] = button;
            buttonObjects[i].transform.GetComponentInChildren<Text>().text = buttons[i].ToString();
        }

        onPause = false;
        startTime = Time.time;
        lastTime=startTime;
        remainTime = gameTime;
        addTime = 0;
        score = 0;
        combo = 0;

        status = Status.InGame;
        RandomCondition();
    }

    private static float addTime = 0;

    private void ToResultScene()
    {
        endTime = Time.time;
            SceneManager.LoadScene("ResultScene");
        
    }
    private void Update()
    {
        if (onPause) return;
        
        if (status == Status.Finish)
        {
            Invoke(nameof(ToResultScene), 2f);
             
            return;
        }
        var spend = Time.time - startTime;
        remainTime = gameTime - spend + addTime;


        if (remainTime <= 0)
        {
            Finish();
        }
    }

    public static void Reset()
    {
        InitGame();
    }

    private static void Finish()
    {
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        if (highScore < score)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            PlayerPrefs.Save();
        }

        status = Status.Finish;
        _onFinish.Invoke();
 
        
 
    }

    static int[] GenRandNumArray()
    {
        var ary = baseArray.OrderBy(n => Guid.NewGuid()).Take(boardSize).ToArray();
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
        }
        else if (combo < 10)
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

        return (int) score;
    }

    public static float quickCorrectAddTime = 1;
    public static float correctAddTime = 1;
    public static float missAddTime = -2;

    public static void OnButtonClick(int num)
    {
        if (status == Status.Finish)
        {
            return;
        }

        // debugButtonNumText.GetComponent<Text>().text = num.ToString();
        var result = judge(num);
        var isQuick = (Time.time - lastTime) < quickTh;
        _onNumButtonClick.Invoke(result, isQuick);
        if (!result)
        {
            if (combo > maxCombo)
            {
                maxCombo = combo;
            }
            combo = 0;
            addTime += missAddTime;
            if (remainTime <= 0)
            {
                Finish();
            }

            return;
        }

        combo++;
        score += CalcScore(combo, isQuick);
        if (isQuick)
        {
        addTime += quickCorrectAddTime;
            
        }
        else
        {
        addTime += correctAddTime;
            
        }

        // ボタン置き換え
        var bList = new List<int>(buttons);

        var btnIdx = bList.IndexOf(num);
        var notInButtonsList = baseArray.Except(buttons).ToArray();
        var newValue = notInButtonsList[UnityEngine.Random.Range(0, notInButtonsList.Length)];
        buttons[btnIdx] = newValue;
        buttonObjects[btnIdx].transform.GetComponentInChildren<Text>().text = newValue.ToString();

        Debug.Log("new value:" + newValue);
        Debug.Log("btnIdx:" + btnIdx);

        // 条件リセット
        RandomCondition();
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

    private static UnityEvent<bool, bool> _onNumButtonClick;

    public static void AddButtonClickListener(UnityAction<bool, bool> a)
    {
        // FinishEventがnullなら作成
        _onNumButtonClick ??= new UnityEvent<bool, bool>();
        _onNumButtonClick.AddListener(a);
    }

    private static UnityEvent _onFinish;

    public static void AddFinishListener(UnityAction a)
    {
        // FinishEventがnullなら作成
        _onFinish ??= new UnityEvent();
        _onFinish.AddListener(a);
    }

    public static bool isFinish()
    {
        return status == Status.Finish;
    }

    private static float pauseStartTime;
    private static bool onPause = false;
    public static void Pause()
    {
        pauseStartTime = Time.time;
        onPause = true;
    }

    public static void EndPause()
    {
        startTime += Time.time-pauseStartTime;
        onPause =false;
    }
}