using UnityEngine;
using UnityEngine.UI;

public class NumButton : Button
{
    protected override void OnClickInternal()
    {
        var text = GetComponentInChildren<Text>().text;
        var i = int.Parse(text);
        GameManager.OnButtonClick(i);
        Debug.Log("tap");
    }
}