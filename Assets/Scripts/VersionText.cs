using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Text>().text = "TapNum " + Application.version;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}