using UnityEngine.SceneManagement;

public class StartButton : Button
{
    protected override void OnClickInternal()
    {
        SceneManager.LoadScene("GameScene");
    }
}