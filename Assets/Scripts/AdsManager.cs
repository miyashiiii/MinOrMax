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
    private const bool TestMode = false;
    private bool done = false;

    void Start()
    {
        if (done) return;
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
        done = true;
        var channelId = "TapNumMinOrMax";
        LocalPushNotification.RegisterChannel(channelId, "TapNumMinOrMax", "説明");
        var notificationTime = 60 * 60 * 24 * 7;
        LocalPushNotification.AddSchedule("Tap Num Min or Max", "Tap Num で遊びませんか？", 1, notificationTime, channelId);

 
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


    public static bool interstitialToggle = false;

    public static void ShowInterstitialIfRegularTiming()
    {
        if (interstitialToggle)
        {
            Advertisement.Show(_interstitialId);
        }

        interstitialToggle = !interstitialToggle;
    }
}