using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    // 2回に1回広告を出す
    private bool _adsToggle = true;

    protected override void OnClickInternal()
    {
        if (_adsToggle)
        {
            AdsManager.ShowInterstitial();
        }

        _adsToggle = !_adsToggle;
        
        SceneManager.LoadScene("GameScene");
    }
}