using UnityEngine.SceneManagement;

public class TitleButton : Button
{
    protected override void OnClickInternal()
    {
        SceneManager.LoadScene("StartScene");
    }
}