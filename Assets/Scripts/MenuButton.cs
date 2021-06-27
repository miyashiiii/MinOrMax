using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject _menuFragment;
    private static GameObject menuFragment;
    private static bool isCountdown;
    private static Sprite _imgMenuButton;
    private static Sprite _imgMenuCloseButton;
    private static Image image;

    private void Start()
    {
        menuFragment = _menuFragment; 
        _imgMenuButton = Resources.Load<Sprite>("MenuButton");
        _imgMenuCloseButton = Resources.Load<Sprite>("MenuCloseButton");
        image = GetComponent<Image>();
    }

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
            image.sprite = _imgMenuButton;
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
            image.sprite = _imgMenuCloseButton;

            AnalyticsEvent.ScreenVisit("Menu");
        }
    }

    public static void MenuClose()
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
        image.sprite = _imgMenuButton;
    }
}