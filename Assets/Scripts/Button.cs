using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioSource onClickSound;

    private void Start()
    {
        onClickSound = GetComponent<AudioSource>();
    }


    public void OnClick()
    {
        if (onClickSound != null) onClickSound.PlayOneShot(onClickSound.clip);

        OnClickInternal();
    }

    protected virtual void OnClickInternal()
    {
    }
}