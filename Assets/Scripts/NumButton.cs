using UnityEngine;
using UnityEngine.UI;

public class NumButton : Button
{
    // Update is called once per frame
    private void Update()
    {
        if (GameManager.isFinish())
        {
            // GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    protected override void OnClickInternal()
    {
        var text = GetComponentInChildren<Text>().text;
        var i = int.Parse(text);
        GameManager.OnButtonClick(i);
        Debug.Log("tap");
    }
}