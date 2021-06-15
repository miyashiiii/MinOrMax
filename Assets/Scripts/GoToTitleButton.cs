using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}