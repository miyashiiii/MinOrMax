using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;

    private bool InFadeOut;

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.bgmManager = this;
        GameManager.AddFinishListener(OnFinish);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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