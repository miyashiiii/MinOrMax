using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class ResultView : MonoBehaviour
{
    public GameObject timeText;
    public GameObject comboText;
    public GameObject scoreText;
    public GameObject newRecordText;


    private void Start()
    {
        var time = (int) (GameManager.EndTime - GameManager.StartTime) - 2;
        if (time <= 0)
        {
            time = 0;
        }

        var mm = (time / 60).ToString("00");
        var ss = (time % 60).ToString("00");
        timeText.GetComponent<Text>().text = mm + ":" + ss;
        var combo = GameManager.MAXCombo;
        var score = GameManager.Score;
        comboText.GetComponent<Text>().text = combo.ToString();
        scoreText.GetComponent<Text>().text = score.ToString();
        var isNewRecord = GameManager.IsNewRecord;
        newRecordText.SetActive(isNewRecord);
        AnalyticsEvent.ScreenVisit("Result");
         
        Debug.Log("--- InAppReview Check ---");
        if (!GameManager.IsNewRecord) return;
        Debug.Log("--- InAppReview isHighScore ---");
 
        var nextReviewHighScore = Util.GetNextReviewHighScore();
        Debug.Log("--- InAppReview next review highScore"+nextReviewHighScore+" ---");
        if (nextReviewHighScore == -1 || nextReviewHighScore > score) return;
        Debug.Log("--- InAppReview ---");
        InAppReview();
        Util.SetNextReviewHighScore();
    }

    private void InAppReview()
    {
        StartCoroutine(Util.ShowReviewCoroutine());
    }
}