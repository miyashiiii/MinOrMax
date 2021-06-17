using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    protected override void OnClickInternal()
    {
        if (Util.GetHighScore() < 30)
        {
            AdsManager.ShowInterstitialIfRegularTiming();
        }


        SceneManager.LoadScene("GameScene");
    }
}