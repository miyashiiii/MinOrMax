using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumButton : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        var text =GetComponentInChildren<Text>().text;
        int i = int.Parse(text);
        GameManager.OnButtonClick(i);

    }
}
