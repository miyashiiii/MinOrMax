using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.AddFinishListener(OnFinish);
    }

    private bool InFadeOut;
    // Update is called once per frame
    void Update()
    {
        if (InFadeOut)
        {
            
        GetComponent<AudioSource>().volume -= 0.1f;
        }
        
    }

    void OnFinish()
    {
        InFadeOut = true;
    }
    
}
