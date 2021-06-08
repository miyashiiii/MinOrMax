using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButton : MonoBehaviour
{
    private AudioSource sound01;

    void Start()
    {
        sound01 = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isFinish())
        {
            // GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    public void OnClick()
    {
        sound01.PlayOneShot(sound01.clip);

        var text =GetComponentInChildren<Text>().text;
        int i = int.Parse(text);
        GameManager.OnButtonClick(i);
        Debug.Log("tap");
        

    }
}
