using UnityEngine;

public class HowToButton : MonoBehaviour
{
    public GameObject HowToFragment;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnClick()
    {
        HowToFragment.SetActive(true);
    }
}