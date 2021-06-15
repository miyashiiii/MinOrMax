using UnityEngine;
using UnityEngine.UI;

public class InfoPanelView : MonoBehaviour
{
    private const int judgeTextAnimationFrames = 50;
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject comboText;
    public GameObject timeText;

    public GameObject debugCondText;
    public GameObject debugCondNumText;
    public GameObject judgeText;
    public GameObject timeUpText;
    private readonly string correctStr = "Good   +" + GameManager.correctAddTime + "sec";
    private int CurrentjudgeTextAnimationFrames;
    private readonly string missStr = "miss...   " + GameManager.missAddTime + "sec";


    private readonly string quickCorrectStr = "Great!   +" + GameManager.quickCorrectAddTime + "sec";

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
        var remainTimeStr = GameManager.remainTime <= 0 ? "0.0" : GameManager.remainTime.ToString("0.0");
        if (GameManager.remainTime < 5)
            timeText.GetComponent<Text>().color = Color.red;
        else
            timeText.GetComponent<Text>().color = Color.black;

        timeText.GetComponent<Text>().text = remainTimeStr;
        comboText.GetComponent<Text>().text = GameManager.combo.ToString();
        scoreText.GetComponent<Text>().text = GameManager.score.ToString();


        if (CurrentjudgeTextAnimationFrames > 0) CurrentjudgeTextAnimationFrames--;
        judgeText.GetComponent<Text>().color =
            new Color(0, 0, 0, (float) CurrentjudgeTextAnimationFrames / judgeTextAnimationFrames);
    }

    private void UpdateCondText()
    {
        Color color;
        string condStr;
        if (GameManager.cond == GameManager.Condition.Max)
        {
            condStr = "▲ Max";
            color = new Color(0.98f, 0.64f, 0.56f);
        }
        else
        {
            condStr = "▼ Min";
            color = new Color(0.36f, 0.8f, 0.9f);
        }

        ;
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

    public void OnButtonClick(bool result, bool isQuick)
    {
        var resultStr = result ? isQuick ? quickCorrectStr : correctStr : missStr;

        judgeText.GetComponent<Text>().text = resultStr;
        CurrentjudgeTextAnimationFrames = judgeTextAnimationFrames;
    }
}