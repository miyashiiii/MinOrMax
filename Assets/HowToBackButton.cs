using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToBackButton : MonoBehaviour
{
    public GameObject HowToFragment;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Onclick()
    {
        HowToFragment.SetActive(false);
 
    }
}
