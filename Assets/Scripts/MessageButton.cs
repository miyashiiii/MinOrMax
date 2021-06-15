using UnityEngine;

public class MessageButton : MonoBehaviour
{
    // public WebViewObject webViewObject;

    public void OnClick()
    {
        Application.OpenURL(Strings.MassageFormUrl);
    }
}