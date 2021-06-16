using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private const string GameIdIos = "4173276";

    private const string GameIdAndroid = "4173277";

    private string _gameId;
    private string _bannerId;
    private static string _interstitialId;
    private const bool TestMode = true;

    void Start()
    {
        Debug.Log("ads initialize start");
#if UNITY_ANDROID
        _gameId = GameIdAndroid;
        _interstitialId = "Interstitial_Android";
        _bannerId = "Banner_Android";
#elif UNITY_IOS
        _gameId = GameIdIos;
        _interstitialId = "Interstitial_iOS";
        _bannerId = "Banner_iOS";
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
            Debug.Log("ads not initialized yet");
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.Show(_bannerId);
        Debug.Log("ads initialized");
    }

    public static void ShowInterstitial()
    {

        Advertisement.Show(_interstitialId);
    }
}