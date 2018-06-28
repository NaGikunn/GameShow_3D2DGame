using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManagerController : SingletonMonoBehaviour<FadeManagerController>
{
    //黒の画像
    public Image BlackImage;
    //白の画像
    public Image WhiteImage;
    //アルファチャンネル
    public static float Whitealfa = 1.0f;
    public static float Blackalfa = 1.0f;

    //白のフェードアウト
    public void WhiteFadeOut()
    {
        Whitealfa -= 0.3f * Time.deltaTime;
        WhiteImage.color = new Color(255, 255, 255, Whitealfa);
    }

    //黒のフェードアウト
    public void BlackFadeOut()
    {
        Blackalfa -= 0.3f * Time.deltaTime;
        BlackImage.color = new Color(0, 0, 0, Blackalfa);
    }

    //白のフェードイン
    public void WhiteFadeIn()
    {
        Whitealfa += 0.4f * Time.deltaTime;
        WhiteImage.color = new Color(255, 255, 255, Whitealfa);
    }

    //黒のフェードイン
    public void BlackFadeIn()
    {
        Blackalfa += 0.5f * Time.deltaTime;
        BlackImage.color = new Color(0, 0, 0, Blackalfa);
    }
}
