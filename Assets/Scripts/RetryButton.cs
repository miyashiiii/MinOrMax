using UnityEngine.SceneManagement;

public class RetryButton : Button
{
    protected override void OnClickInternal()
    {
        SceneManager.LoadScene("GameScene");
    }
}