using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public GameObject menuFragment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
