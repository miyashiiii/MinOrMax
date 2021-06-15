using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;

    private bool _inFadeOut;

    private void Start()
    {
        GameManager.BGMManager = this;
        GameManager.AddFinishListener(OnFinish);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_inFadeOut)
            audioSource.volume -= 0.1f;
        else if (GameManager.ONPause)
            audioSource.volume = 0;
        else
            audioSource.volume = 1;
    }

    private void OnFinish()
    {
        _inFadeOut = true;
    }
}