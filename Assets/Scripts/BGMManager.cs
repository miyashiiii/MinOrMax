using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;

    private bool InFadeOut;

    private void Start()
    {
        GameManager.bgmManager = this;
        GameManager.AddFinishListener(OnFinish);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (InFadeOut)
            audioSource.volume -= 0.1f;
        else if (GameManager.onPause)
            audioSource.volume = 0;
        else
            audioSource.volume = 1;
    }

    private void OnFinish()
    {
        InFadeOut = true;
    }
}