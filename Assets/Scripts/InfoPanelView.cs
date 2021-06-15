using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    private const int JudgeTextAnimationFrames = 50;
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject comboText;
    public GameObject timeText;

    public GameObject debugCondText;
    public GameObject debugCondNumText;
    public GameObject judgeText;
    public GameObject timeUpText;
    private readonly string _correctStr = "Good   +" + GameManager.CorrectAddTime + "sec";
    private readonly string _missStr = "miss...   " + GameManager.MissAddTime + "sec";


    private readonly string _quickCorrectStr = "Great!   +" + GameManager.QuickCorrectAddTime + "sec";
    private int _currentjudgeTextAnimationFrames;

    private void Start()
    {
        timeUpText.SetActive(false);


        UpdateCondText();
        GameManager.AddFinishListener(Onfinish);
        GameManager.AddButtonClickListener(OnButtonClick);
        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }

    private void Update()
    {
        UpdateCondText();
        var remainTimeStr = GameManager.RemainTime <= 0 ? "0.0" : GameManager.RemainTime.ToString("0.0");
        timeText.GetComponent<Text>().color = GameManager.RemainTime < 5 ? Color.red : Color.black;

        timeText.GetComponent<Text>().text = remainTimeStr;
        comboText.GetComponent<Text>().text = GameManager.Combo.ToString();
        scoreText.GetComponent<Text>().text = GameManager.Score.ToString();


        if (_currentjudgeTextAnimationFrames > 0) _currentjudgeTextAnimationFrames--;
        judgeText.GetComponent<Text>().color =
            new Color(0, 0, 0, (float) _currentjudgeTextAnimationFrames / JudgeTextAnimationFrames);
    }

    private void UpdateCondText()
    {
        Color color;
        string condStr;
        if (GameManager.Cond == GameManager.Condition.Max)
        {
            condStr = "▲ Max";
            color = new Color(0.98f, 0.64f, 0.56f);
        }
        else
        {
            condStr = "▼ Min";
            color = new Color(0.36f, 0.8f, 0.9f);
        }

        debugCondText.GetComponentInChildren<Text>().text = condStr;
        debugCondText.GetComponent<Image>().color = color;
    }

    private void Onfinish()
    {
        timeUpText.SetActive(true);
        UpdateCondText();

        var highScore = PlayerPrefs.GetInt("HIGH_SCORE");

        highScoreText.GetComponent<Text>().text = highScore.ToString();
    }

    private void OnButtonClick(bool result, bool isQuick)
    {
        var resultStr = result ? isQuick ? _quickCorrectStr : _correctStr : _missStr;

        judgeText.GetComponent<Text>().text = resultStr;
        _currentjudgeTextAnimationFrames = JudgeTextAnimationFrames;
    }
}