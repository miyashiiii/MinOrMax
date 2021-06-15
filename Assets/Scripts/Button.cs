using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    protected AudioSource onClickSound;

    // Start is called before the first frame update
    private void Start()
    {
        onClickSound = GetComponent<AudioSource>();
    }


    public void OnClick()
    {
        if (onClickSound != null)
        {
            onClickSound.PlayOneShot(onClickSound.clip);
        }

        OnClickInternal();
    }

    protected virtual void OnClickInternal()
    {
    }
}