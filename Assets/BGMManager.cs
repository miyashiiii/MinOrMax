using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BGMManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.bgmManager = this;
        GameManager.AddFinishListener(OnFinish);
        audioSource = GetComponent<AudioSource>();
        
    }

    private bool InFadeOut;

    public AudioSource audioSource;
    // Update is called once per frame
    void Update()
    {
        if (InFadeOut)
        {
            
            audioSource.volume -= 0.1f;
        }else if(GameManager.onPause)

        {
            
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = 1;
            
        }
        
    }

    void OnFinish()
    {
        InFadeOut = true;
    }
    
}
