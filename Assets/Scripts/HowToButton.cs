using UnityEngine;

public class HowToButton : MonoBehaviour
{
    public GameObject HowToFragment;

    public void OnClick()
    {
        HowToFragment.SetActive(true);
    }
}