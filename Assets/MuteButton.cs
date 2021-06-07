using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private static bool isAudio = true;
    private Sprite imgAudioOn;
    private Sprite imgAudioOff;

    // Start is called before the first frame update
    void Start()
    {
    imgAudioOn = Resources.Load<Sprite>("AudioOn");
    imgAudioOff = Resources.Load<Sprite>("AudioOff");

        applyAudioConfig();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    
    {
        isAudio = !isAudio;
        applyAudioConfig();
    }

    void applyAudioConfig()
    {
    var image= GetComponent<Image>();
        if (isAudio)
        
        {
            AudioListener.volume = 1;
            image.sprite = imgAudioOn;
            image.color = new Color(0.98f, 0.64f, 0.56f);

        }
        else
        {
            AudioListener.volume = 0;
            image.sprite = imgAudioOff;
            image.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
}