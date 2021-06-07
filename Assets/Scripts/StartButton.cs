using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private AudioSource sound01;

    // Start is called before the first frame update
    void Start()
    {
               sound01 = GetComponent<AudioSource>();
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        sound01.PlayOneShot(sound01.clip);
 
        SceneManager.LoadScene("GameScene");

    }
}
