using UnityEngine;
using UnityEngine.Analytics;

public class MessageButton : MonoBehaviour
{
    public void OnClick()
    {
        Application.OpenURL(Strings.MassageFormUrl);
        AnalyticsEvent.ScreenVisit("SendMessage");
 
    }
}