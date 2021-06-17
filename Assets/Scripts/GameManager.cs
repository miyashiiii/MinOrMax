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

    private const int MAXNum = 30;
    private static int[] _buttons;

    public static int Score;
    public static int Combo;
    public static int MAXCombo;
    public static float StartTime;
    public static float EndTime;
    private static readonly IEnumerable<int> BaseArray = Enumerable.Range(1, MAXNum);
    private static readonly GameObject[] ButtonObjects = new GameObject[16];

    public static bool IsNewRecord;
    public static BGMManager BGMManager;

    public static Condition Cond = Condition.Min;
    private static int condNum = 1;

    private static float _lastTime;
    private const float QuickTh = 1f;
    private const float BaseSuccessScore = 10f;

    private const float GameTime = 20;
    public static float RemainTime;
    private const int BoardSideSize = 2;
    private const int BoardSize = BoardSideSize * BoardSideSize;

    private static Status _status = Status.InGame;
    private static Transform _buttonsTransform;

    private static float _addTime;

    public const float QuickCorrectAddTime = 1;
    public const float CorrectAddTime = 1;
    public const float MissAddTime = -2;

    private static UnityEvent<bool, bool> _onNumButtonClick;

    private static UnityEvent _onFinish;

    private static float _pauseStartTime;
    public static bool ONPause;

    // ReSharper disable once Unity.IncorrectMethodSignature
    public static void Reset()
    {
        InitGame();
    }

    private void Start()
    {
        _buttonsTransform = transform;
    }

    private void Update()
    {
        if (ONPause) return;

        if (_status == Status.Finish)
        {
            Invoke(nameof(ToResultScene), 2f);

            return;
        }

        var spend = Time.time - StartTime;
        RemainTime = GameTime - spend + _addTime;


        if (RemainTime <= 0) Finish();
    }

    public static void InitGame()
    {
        _buttons = GenRandNumArray();
        for (var i = 0; i < BoardSize; i++)
        {
            var button = _buttonsTransform.GetChild(i).gameObject;
            ButtonObjects[i] = button;
            ButtonObjects[i].transform.GetComponentInChildren<Text>().text = _buttons[i].ToString();
        }

        ONPause = false;
        StartTime = Time.time;
        _lastTime = StartTime;
        RemainTime = GameTime;
        _addTime = 0;
        Score = 0;
        Combo = 0;
        BGMManager.audioSource.Play();
        IsNewRecord = false;
        _status = Status.InGame;
        RandomCondition();
    }

    private void ToResultScene()
    {
        EndTime = Time.time;
        SceneManager.LoadScene("ResultScene");
    }

    private static void Finish()
    {

        if (Util.GetHighScore() < Score)
        {
            Util.SetHighScore(Score);
            IsNewRecord = true;
        }

        _status = Status.Finish;
        _onFinish.Invoke();
    }

    private static int[] GenRandNumArray()
    {
        var ary = BaseArray.OrderBy(n => Guid.NewGuid()).Take(BoardSize).ToArray();
        return ary;
    }

    private static int CalcScore(int combo, bool isQuick)
    {
        var score = BaseSuccessScore;

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
        if (_status == Status.Finish) return;

        // debugButtonNumText.GetComponent<Text>().text = num.ToString();
        var result = Judge(num);
        var isQuick = Time.time - _lastTime < QuickTh;
        _onNumButtonClick.Invoke(result, isQuick);
        if (!result)
        {
            if (Combo > MAXCombo) MAXCombo = Combo;
            Combo = 0;
            _addTime += MissAddTime;
            if (RemainTime <= 0) Finish();

            return;
        }

        Combo++;
        Score += CalcScore(Combo, isQuick);
        if (isQuick)
            _addTime += QuickCorrectAddTime;
        else
            _addTime += CorrectAddTime;

        // ボタン置き換え
        var bList = new List<int>(_buttons);

        var btnIdx = bList.IndexOf(num);
        var notInButtonsList = BaseArray.Except(_buttons).ToArray();
        var newValue = notInButtonsList[Random.Range(0, notInButtonsList.Length)];
        _buttons[btnIdx] = newValue;
        ButtonObjects[btnIdx].transform.GetComponentInChildren<Text>().text = newValue.ToString();

        Debug.Log("new value:" + newValue);
        Debug.Log("btnIdx:" + btnIdx);

        // 条件リセット
        RandomCondition();
        _lastTime = Time.time;
    }

    
    private static bool Judge(int num)
    {
        var bList = new List<int>(_buttons);
        switch (Cond)
        {
            case Condition.Max when bList.Max() == num:
            case Condition.Min when bList.Min() == num:
                return true;
            default:
                return false;
        }
    }

    private static void RandomCondition()
    {
        var values = Enum.GetValues(typeof(Condition));
        var random1 = new System.Random();
        Cond = (Condition) values.GetValue(random1.Next(values.Length));
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

    public static void Pause()
    {
        _pauseStartTime = Time.time;
        ONPause = true;
    }

    public static void EndPause()
    {
        StartTime += Time.time - _pauseStartTime;
        ONPause = false;
    }
}