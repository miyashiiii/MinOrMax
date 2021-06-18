using UnityEngine;
using UnityEngine.Analytics;

public class MenuButton : MonoBehaviour
{
    public GameObject menuFragment;
    private bool isCountdown;

    public void OnClick()
    {
        var isActive = menuFragment.activeInHierarchy;
        if (isActive)
        {
            if (isCountdown)
            {
                isCountdown = false;
            }
            else

            {
                GameManager.EndPause();
            }

            menuFragment.SetActive(false);
        }
        else
        {
            if (GameManager.ONPause)
            {
                isCountdown = true;
            }
            else
            {
                GameManager.Pause();
            }

            menuFragment.SetActive(true);
            AnalyticsEvent.ScreenVisit("Menu");
        }
    }
}