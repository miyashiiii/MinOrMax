using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum Condition
    {
        Min,
        Max
    }

    public enum Status
    {
        InGame,
        Finish
    }

    private const int maxNum = 30;
    private static int pressedScore;
    private static int[] buttons;

    public static int score;
    public static int combo;
    public static int maxCombo;
    public static float startTime;
    public static float endTime;
    private static IEnumerable<int> baseArray = Enumerable.Range(1, maxNum);
    private static GameObject[] buttonObjects = new GameObject[16];

    public static bool isNewRecord;
    public static BGMManager bgmManager;

    public static Condition cond = Condition.Min;
    private static int condNum = 1;

    private static float lastTime;
    private const float quickTh = 1f;
    private const float baseSuccessScore = 10f;

    private const float gameTime = 20;
    public static float remainTime;
    private const int boardSideSize = 2;
    private static readonly int boardSize = boardSideSize * boardSideSize;

    private static Status status = Status.InGame;
    private static Transform buttonsTransform;

    private static float addTime;

    public const float quickCorrectAddTime = 1;
    public const float correctAddTime = 1;
    public const float missAddTime = -2;

    private static UnityEvent<bool, bool> _onNumButtonClick;

    private static UnityEvent _onFinish;

    private static float pauseStartTime;
    public static bool onPause;

    // ReSharper disable once Unity.IncorrectMethodSignature
    public static void Reset()
    {
        InitGame();
    }

    private void Start()
    {
        buttonsTransform = transform;
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


        if (remainTime <= 0) Finish();
    }

    public static void InitGame()
    {
        buttons = GenRandNumArray();
        for (var i = 0; i < boardSize; i++)
        {
            var button = buttonsTransform.GetChild(i).gameObject;
            buttonObjects[i] = button;
            buttonObjects[i].transform.GetComponentInChildren<Text>().text = buttons[i].ToString();
        }

        onPause = false;
        startTime = Time.time;
        lastTime = startTime;
        remainTime = gameTime;
        addTime = 0;
        score = 0;
        combo = 0;
        bgmManager.audioSource.Play();
        isNewRecord = false;
        status = Status.InGame;
        RandomCondition();
    }

    private void ToResultScene()
    {
        endTime = Time.time;
        SceneManager.LoadScene("ResultScene");
    }

    private static void Finish()
    {
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        if (highScore < score)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            PlayerPrefs.Save();
            isNewRecord = true;
        }

        status = Status.Finish;
        _onFinish.Invoke();
    }

    private static int[] GenRandNumArray()
    {
        var ary = baseArray.OrderBy(n => Guid.NewGuid()).Take(boardSize).ToArray();
        return ary;
    }

    private static int CalcScore(int combo, bool isQuick)
    {
        var score = baseSuccessScore;

        if (isQuick) score *= 1.5f;

        if (combo < 5)
            score *= 1f;
        else if (combo < 10)
            score *= 1.2f;
        else if (combo < 20)
            score *= 1.5f;
        else if (combo < 30)
            score *= 1.8f;
        else
            score *= 2f;

        return (int) score;
    }

    public static void OnButtonClick(int num)
    {
        if (status == Status.Finish) return;

        // debugButtonNumText.GetComponent<Text>().text = num.ToString();
        var result = judge(num);
        var isQuick = Time.time - lastTime < quickTh;
        _onNumButtonClick.Invoke(result, isQuick);
        if (!result)
        {
            if (combo > maxCombo) maxCombo = combo;
            combo = 0;
            addTime += missAddTime;
            if (remainTime <= 0) Finish();

            return;
        }

        combo++;
        score += CalcScore(combo, isQuick);
        if (isQuick)
            addTime += quickCorrectAddTime;
        else
            addTime += correctAddTime;

        // ボタン置き換え
        var bList = new List<int>(buttons);

        var btnIdx = bList.IndexOf(num);
        var notInButtonsList = baseArray.Except(buttons).ToArray();
        var newValue = notInButtonsList[Random.Range(0, notInButtonsList.Length)];
        buttons[btnIdx] = newValue;
        buttonObjects[btnIdx].transform.GetComponentInChildren<Text>().text = newValue.ToString();

        Debug.Log("new value:" + newValue);
        Debug.Log("btnIdx:" + btnIdx);

        // 条件リセット
        RandomCondition();
        lastTime = Time.time;
    }

    private static bool judge(int num)
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
        var values = Enum.GetValues(typeof(Condition));
        var random1 = new System.Random();
        cond = (Condition) values.GetValue(random1.Next(values.Length));
        var random2 = new System.Random();
        condNum = random2.Next(1, 2); //TODO
        // condNum=random2.Next(1, 3);
    }

    public static void AddButtonClickListener(UnityAction<bool, bool> a)
    {
        // FinishEventがnullなら作成
        _onNumButtonClick ??= new UnityEvent<bool, bool>();
        _onNumButtonClick.AddListener(a);
    }

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

    public static void Pause()
    {
        pauseStartTime = Time.time;
        onPause = true;
    }

    public static void EndPause()
    {
        startTime += Time.time - pauseStartTime;
        onPause = false;
    }
}