using UnityEngine;

public class HowToBackButton : MonoBehaviour
{
    public GameObject HowToFragment;

    public void Onclick()
    {
        HowToFragment.SetActive(false);
    }
}