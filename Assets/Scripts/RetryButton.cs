using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    protected override void OnClickInternal()
    {
        if (Util.GetHighScore() > 30)
        {
            AdsManager.ShowInterstitialIfRegularTiming();
            AnalyticsEvent.AdStart(false);
        }


        SceneManager.LoadScene("GameScene");
    }

}