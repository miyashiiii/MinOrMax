using UnityEngine;
using UnityEngine.Analytics;

public class HowToButton : MonoBehaviour
{
    public GameObject howToFragment;

    public void OnClick()
    {
        howToFragment.SetActive(true);
        AnalyticsEvent.ScreenVisit("HowToPlay");
    }
}