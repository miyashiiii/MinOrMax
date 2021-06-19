using System.Collections;
using System.Collections.Generic;
using Google.Play.Review;
using UnityEngine;
using UnityEngine.Analytics;



public static class Util
{
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HIGH_SCORE");
    }

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("HIGH_SCORE", score);
        PlayerPrefs.Save();
    }

    private static int[] reviewHighScoreOrder = new[] {0, 10, 50, 100};
    // private static int[] reviewHighScoreOrder = new[] {0, 500, 100, 2000};
    public static int GetNextReviewHighScore() {
        
        var idx=PlayerPrefs.GetInt("NEXT_REVIEW_HIGH_SCORE_IDX", 0);
        int value;
        if (idx < 0)
        {
            value = -1;
        }
        else
        {
            value = reviewHighScoreOrder[idx];
        }
        return value;
    }
    public static void SetNextReviewHighScore() {
        var current=PlayerPrefs.GetInt("NEXT_REVIEW_HIGH_SCORE_IDX", 0);

        var next = current + 1;
        if (current <0&& current>=3)
        {
            next = -1;
        }

        PlayerPrefs.SetInt("NEXT_REVIEW_HIGH_SCORE_IDX", next);
        PlayerPrefs.Save();
    }
    


    /// <summary>
    /// Android端末でIn-App Review APIを呼ぶサンプル
    /// </summary>
    public static IEnumerator ShowReviewCoroutine()
        {
            // https://developer.android.com/guide/playcore/in-app-review/unity
            var reviewManager = new ReviewManager();
            var requestFlowOperation = reviewManager.RequestReviewFlow();
            yield return requestFlowOperation;
            if (requestFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // エラーの場合はここで止まる.
                yield break;
            }
            var playReviewInfo = requestFlowOperation.GetResult(); 
            var launchFlowOperation = reviewManager.LaunchReviewFlow(playReviewInfo);
            yield return launchFlowOperation;
            if (launchFlowOperation.Error != ReviewErrorCode.NoError)
            {
                // エラーの場合はここで止まる.
                yield break;
            }
        }
}