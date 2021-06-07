using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using Shapes2D;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public  GameObject scoreTextTmp;
    public  GameObject highScoreTextTmp;
    public  GameObject comboTextTmp;
    public  GameObject timeTextTmp;
    
    public  GameObject debugButtonNumTextTmp;
    public  GameObject debugCondTextTmp;
    public  GameObject debugCondNumTextTmp;
    public  GameObject debugJudgeTextTmp;

    public  GameObject scoreText;
    public  GameObject highScoreText;
    public  GameObject comboText;
    public  GameObject timeText;
    
    public  GameObject debugButtonNumText;
    public  GameObject debugCondText;
    public  GameObject debugCondNumText;
    public  GameObject debugJudgeText;
    public int[]buttons;
     public static IEnumerable<int> baseArray = Enumerable.Range(1, 41);
    // Start is called before the first frame update
        public static GameObject[] buttonObjects = new GameObject[16];
    void Start()
    {
     scoreText=scoreTextTmp;
     highScoreText=highScoreTextTmp;
     comboText=comboTextTmp;
     timeText=timeTextTmp;
    
     debugButtonNumText=debugButtonNumTextTmp;
     debugCondText=debugCondTextTmp;
     debugCondNumText=debugCondNumTextTmp;
     debugJudgeText=debugJudgeTextTmp;
       
        
        
        debugCondText.GetComponent<Text>().text = Enum.GetName(typeof(GameManager.Condition), GameManager.cond);
        debugCondNumText.GetComponent<Text>().text = GameManager.condNum.ToString();

        int[]randNumArray=GenRandNumArray();
        GameManager.buttons = randNumArray;
        for (int i = 0; i < 16; i++)
        {
            var button = transform.GetChild(i).gameObject;
            buttonObjects[i] = button;
            buttonObjects[i].transform.GetComponentInChildren<Text>().text =randNumArray[i].ToString(); 
        }

    }

    int[] GenRandNumArray()
    {
 
        var ary = baseArray.OrderBy(n => Guid.NewGuid()).Take(16).ToArray();
        return ary;
    }

    // Update is called once per frame
    void Update()
    {
    }
}