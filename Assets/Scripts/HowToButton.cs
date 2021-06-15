using UnityEngine;

public class HowToButton : MonoBehaviour
{
    public GameObject howToFragment;

    public void OnClick()
    {
        howToFragment.SetActive(true);
    }
}