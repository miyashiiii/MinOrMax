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

    public static bool IsReview()
    {
        var count = PlayerPrefs.GetInt("HIGH_SCORE_UPDATE_COUNT", 0);
        return count % 4 == 0;
    }
}