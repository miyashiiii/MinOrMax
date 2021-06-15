using UnityEngine;
using UnityEngine.UI;

public class CountDownView : MonoBehaviour
{
    public GameObject menuFragment;

    // Start is called before the first frame update
    private Text countDownText;
    private float menuStartTime;

    private bool onMenu;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        countDownText = GetComponentInChildren<Text>();
        GameManager.Pause();
    }

    // Update is called once per frame
    private void Update()
    {
        if (menuFragment.activeSelf)
        {
            if (!onMenu) onMenu = true;
            return;
        }

        if (onMenu)
        {
            var menuTime = Time.time - startTime;
            startTime += menuTime;
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

        countDownText.text = (3 - (int) time).ToString();
    }
}