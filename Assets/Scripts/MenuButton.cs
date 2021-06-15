using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject menuFragment;


    public void OnClick()
    {
        var isActive = menuFragment.activeSelf;
        if (isActive)
        {
            GameManager.EndPause();
            menuFragment.SetActive(false);
        }
        else
        {
            GameManager.Pause();
            menuFragment.SetActive(true);
        }
    }
}