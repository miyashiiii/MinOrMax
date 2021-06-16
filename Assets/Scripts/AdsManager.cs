using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    private const string GameIdIos = "4173276";
    private const string GameIdAndroid = "4173277";
    private const string surfacingId = "Banner_Android";
    private const bool TestMode = true;

    void Start()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(GameIdAndroid,TestMode);
#elif UNITY_IOS
            Advertisement.Initialize(GameIdIos);
#else
#endif
        
    Advertisement.Banner.SetPosition (BannerPosition.BOTTOM_CENTER);

        StartCoroutine(ShowBannerWhenInitialized());
    }

    IEnumerator ShowBannerWhenInitialized () {
        while (!Advertisement.isInitialized) {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show (surfacingId);
    } 
 
 
}