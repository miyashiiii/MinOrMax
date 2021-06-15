using UnityEngine;

public class MessageButton : MonoBehaviour
{
    public void OnClick()
    {
        Application.OpenURL(Strings.MassageFormUrl);
    }
}