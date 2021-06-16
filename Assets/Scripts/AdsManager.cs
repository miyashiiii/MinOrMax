using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private const string GameIdIos = "4173276";

    private const string GameIdAndroid = "4173277";

    // private const string BannerId = "Banner_Android";
    private string _gameId;
    private string _bannerId;
    private static string _interstitialId;
    private const bool TestMode = true;

    void Start()
    {
#if UNITY_ANDROID
        _gameId = GameIdAndroid;
        _interstitialId = "Interstitial_Android";
        _bannerId = "Banner_Android";
#elif UNITY_IOS
        _gameId = GameIdIos;
        _interstitialId = "Interstitial_Ios";
        _bannerId = "Banner_Ios";
#else
        return;
#endif

        Advertisement.Initialize(_gameId, TestMode);

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        StartCoroutine(ShowBannerWhenInitialized());
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show(_bannerId);
    }

    public static void ShowInterstitial()
    {
        Advertisement.Show(_interstitialId);
    }
}