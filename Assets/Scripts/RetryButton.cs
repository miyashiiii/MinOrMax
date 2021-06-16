using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    // 2回に1回広告を出す
    private bool _adsToggle = false;

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