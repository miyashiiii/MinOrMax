using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioSource _onClickSound;

    private void Start()
    {
        _onClickSound = GetComponent<AudioSource>();
    }


    public void OnClick()
    {
        if (_onClickSound != null) _onClickSound.PlayOneShot(_onClickSound.clip);

        OnClickInternal();
    }

    protected virtual void OnClickInternal()
    {
    }
}