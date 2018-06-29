using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCount : MonoBehaviour {
    float time;
    Text text;
	// Use this for initialization
	void Start () {
        time = 0;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        int minute = (int)time / 60;//分
        int second = (int)time % 60;//秒
        string minText, secText;
        if (minute < 10)
            minText = "0" + minute.ToString();//ToStringでint→stringに変換.
        else
            minText = minute.ToString();
        if (second < 10)
            secText = "0" + second.ToString();//上に同じく.
        else
            secText = second.ToString();

        text.text = "[Time] " + minText + "：" + secText;
    }
}
