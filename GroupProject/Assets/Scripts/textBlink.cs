using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class textBlink : MonoBehaviour
{

    public TextMeshProUGUI text;
    public float BlinkFadeInTime = 6.8f;
    public float BlinkStayTime = 5.3f;
    public float BlinkFadeOutTime = 1.9f;
    private float timeChecker = 0;
    private Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = text.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeChecker += Time.deltaTime;
        if(timeChecker < BlinkFadeInTime)
        {
            text.color = new Color(color.r, color.g, color.b, timeChecker / BlinkFadeInTime);

        } else if (timeChecker < BlinkFadeInTime + BlinkStayTime)
        {
            text.color = new Color(color.r, color.g, color.b, 1);
        }
        else if (timeChecker < BlinkFadeInTime + BlinkStayTime + BlinkFadeOutTime)
        {
            text.color = new Color(color.r, color.g, color.b, 1 - (timeChecker - (BlinkFadeInTime + BlinkStayTime))/BlinkFadeInTime);
        }
    }
}

