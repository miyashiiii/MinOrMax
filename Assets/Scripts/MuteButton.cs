using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private static bool _isAudio = true;
    private Sprite _imgAudioOff;
    private Sprite _imgAudioOn;

    private void Start()
    {
        _imgAudioOn = Resources.Load<Sprite>("AudioOn");
        _imgAudioOff = Resources.Load<Sprite>("AudioOff");

        ApplyAudioConfig();
    }


    public void OnClick()

    {
        _isAudio = !_isAudio;
        ApplyAudioConfig();
    }

    private void ApplyAudioConfig()
    {
        var image = GetComponent<Image>();
        if (_isAudio)

        {
            AudioListener.volume = 1;
            image.sprite = _imgAudioOn;
            image.color = new Color(0.98f, 0.64f, 0.56f);
        }
        else
        {
            AudioListener.volume = 0;
            image.sprite = _imgAudioOff;
            image.color = new Color(0.6f, 0.6f, 0.6f);
        }
    }
}