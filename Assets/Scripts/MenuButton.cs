using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public GameObject menuFragment;
    private bool isCountdown;
    private Sprite _imgMenuButton;
    private Sprite _imgMenuCloseButton;
    
    private void Start()
    {
        _imgMenuButton = Resources.Load<Sprite>("MenuButton");
        _imgMenuCloseButton = Resources.Load<Sprite>("MenuCloseButton");
    }

    public void OnClick()
    {
        var image = GetComponent<Image>();
 
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
}