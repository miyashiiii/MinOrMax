using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownView : MonoBehaviour
{
    public GameObject menuFragment;
    private float startTime;
    private float menuStartTime;
    
    // Start is called before the first frame update
    private Text countDownText; 
    void Start()
    {
        startTime = Time.time;
        countDownText = GetComponentInChildren<Text>();
        GameManager.Pause();
    }

    private bool onMenu;
    // Update is called once per frame
    void Update()
    {
        if (menuFragment.activeSelf)
        {
            if (!onMenu) onMenu = true;
            return;
        }
        if(onMenu)
        {
            var menuTime = Time.time - startTime;
            startTime +=menuTime;
            onMenu = false;
        }
        var time = Time.time - startTime;
        if (time >= 3)
        {
        gameObject.SetActive(false);
        GameManager.EndPause();
        GameManager.InitGame();
        return;
        }

        countDownText.text = (3-(int) time).ToString();
    }

}
