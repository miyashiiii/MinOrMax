using UnityEngine;
using UnityEngine.UI;

public class CountDownView : MonoBehaviour
{
    public GameObject menuFragment;

    private Text _countDownText;
    private float menuStartTime=0;

    private bool _onMenu;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
        _countDownText = GetComponentInChildren<Text>();
        GameManager.Pause();
    }

    // Update is called once per frame
    private void Update()
    {
        if (menuFragment.activeInHierarchy)
        {
            if (!_onMenu)
            {
                _onMenu = true;
                menuStartTime = Time.time;
            }
            return;
        }

        if (_onMenu)
        {
            var menuTime = Time.time - menuStartTime;
            _startTime += menuTime;
            _onMenu = false;
        }

        var time = Time.time - _startTime;
        if (time >= 3)
        {
            gameObject.SetActive(false);
            GameManager.EndPause();
            GameManager.InitGame();
            return;
        }

        _countDownText.text = (3 - (int) time).ToString();
    }
}