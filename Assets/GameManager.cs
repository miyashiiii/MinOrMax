using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private static int pressedScore;
    public static int[] buttons;

    public static int score = 0;
    public static int combo = 0;
    public static float startTime;


    public static IEnumerable<int> baseArray = Enumerable.Range(1, 40);
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

    public static int gameTime = 5;
    public static int remainTime;
    static int boardSideSize = 4;
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


        InitGame();
    }

    private static void InitGame()
    {
        buttons = GenRandNumArray();
        for (int i = 0; i < boardSize; i++)
        {
            var button = buttonsTransform.GetChild(i).gameObject;
            buttonObjects[i] = button;
            buttonObjects[i].transform.GetComponentInChildren<Text>().text = buttons[i].ToString();
        }

        startTime = Time.time;
        remainTime = gameTime;
        addTime = 0;
        score = 0;
        combo = 0;

        status = Status.InGame;
    }

    private static int addTime = 0;

    private void Update()
    {
        if (status == Status.Finish)
        {
            return;
        }

        var spend = (int) (Time.time - startTime);
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

    private void Finish()
    {
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

    public static void OnButtonClick(int num)
    {
        if (status == Status.Finish)
        {
            return;
        }

        // debugButtonNumText.GetComponent<Text>().text = num.ToString();
        var result = judge(num);
        var isQuick = (Time.time - lastTime) < quickTh;
        if (result)
        {
            combo++;
            score += CalcScore(combo, isQuick);
            addTime += 1;
        }
        else
        {
            combo = 0;
            addTime -= 5;
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
        _onNumButtonClick.Invoke(result, isQuick);
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
}