using UnityEngine;

public class HowToBackButton : MonoBehaviour
{
    public GameObject HowToFragment;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    public void Onclick()
    {
        HowToFragment.SetActive(false);
    }
}