using UnityEngine;

public class HowToBackButton : MonoBehaviour
{
    public GameObject howToFragment;

    public void Onclick()
    {
        howToFragment.SetActive(false);
    }
}