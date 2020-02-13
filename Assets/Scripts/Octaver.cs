using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Octaver : MonoBehaviour
{
    public Text octaveText;
    public GameObject keys;
    public Transform leftref, rightref, firstpos, lastpos;
    Slider sl;
    Stage st;
    public Sprite on, off;

    private void Start()
    {
        float leftdis = Vector2.Distance(firstpos.position, leftref.position);
        float rightdis = Vector2.Distance(lastpos.position, rightref.position);

        sl = GetComponent<Slider>();
        st = FindObjectOfType<Stage>();

        sl.minValue = rightdis * -100;
        sl.maxValue = leftdis * 100;
    }

    public static bool rotaryBool = false;
    public static bool got = false;

    public void ValueChanged()
    {
        //24.86
        //12,48
        //0
        //-12,42
        //-21

        float val = sl.value/100;

        if(val<=24.86 && val > 12.48)
        {
            octaveText.text = "C1";
        }
        if(val<=12.48 && val > 0)
        {
            octaveText.text = "C2";
        }
        if(val<=0 && val > -12.42)
        {
            octaveText.text = "C3";
        }
        if (val <= -12.42 && val > -20)
        {
            octaveText.text = "C4";
        }
        if (val <= -20)
        {
            octaveText.text = "C5";
        }

        keys.transform.position = new Vector2(val, keys.transform.position.y);
    }
}
