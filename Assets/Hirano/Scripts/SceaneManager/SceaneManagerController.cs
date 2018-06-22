using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SceaneManagerController : SingletonMonoBehaviour<SceaneManagerController>
{
    //黒の画像
    public Image BlackImage;
    //白の画像
    public Image WhiteImage;
    //現在のシーン名
    private string NowScene;
    //アルファチャンネル
    public float Whitealfa = 1.0f;
    public float Blackalfa = 1.0f;
    private AudioSource BGM;
    private bool sceaneflg;
    private float TIME = 0.0f;
    void Start()
    {
        TIME = 0.0f;
        BGM = GetComponent<AudioSource>();
        sceaneflg = false;
    }

    public void  NowSceneManagement()
    {

        //現在のシーン
        NowScene = SceneManager.GetActiveScene().name;
        //タイトルなら
        if (NowScene == "Title")
        {
            if (Input.anyKeyDown)
            {
                sceaneflg = true;
            }

            if (Whitealfa >= 0.0f)
            {
                WhiteFadeOut();
            }
            //すべてのボタン
            if (sceaneflg)
            {
                TIME += Time.deltaTime;
                if (Blackalfa <= 1.0f)
                {
                    BlackFadeIn();
                }
                if (BGM.volume >= 0.0f)
                {
                    BGMFadeOut();
                }
                if (TIME >= 3.0f)
                {
                    SceneManager.LoadScene("StageSelect");
                }
            }
        }

        //セレクト画面なら
        if (NowScene == "StageSelect")
        {
            if (Blackalfa >= 0.0f)
            {
                BlackFadeOut();
            }
        }

        //チュートリアルステージなら
        if (NowScene == "StageTutorial_m")
        {
        //初めに黒のフェードアウト
        if (Blackalfa >= 0.0f)
        {
             BlackFadeOut();
        }

        //clear時
        if (PlayerMoveController.Clear)
        {
            if (Whitealfa <= 1.0f)
            {
               WhiteFadeIn();
            }
        }
    }
}

    public void BGMFadeOut()
    {
        BGM.volume -= 0.3f * Time.deltaTime;
    }

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
        Blackalfa += 0.3f * Time.deltaTime;
        BlackImage.color = new Color(0, 0, 0, Blackalfa);
    }
}
