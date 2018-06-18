using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void NowSceneManagement()
    {
        //現在のシーン
        NowScene = SceneManager.GetActiveScene().name;
        //タイトルなら
        if (NowScene == "Title")
        {
            if (Whitealfa >= 0.0f)
            {
                WhiteFadeOut();
            }
            //すべてのボタン
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("StageSelect");
            }
        }

        //セレクト画面なら
        if(NowScene == "StageSelect")
        {
            
            if(Blackalfa >= 0.0f)
            {
                BlackFadeOut();
            }

        }

        //チュートリアルステージなら
        if(NowScene == "StageTutorial_m")
        {
            if (Blackalfa >= 0.0f)
            {
                BlackFadeOut();
            }

            if (PlayerMoveController.Clear)
            {
                if(Whitealfa <= 1.0f)
                {
                    WhiteFadeIn();
                    Debug.Log("Clear");
                }
            }
        }
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
        Whitealfa += 0.3f * Time.deltaTime;
        WhiteImage.color = new Color(255, 255, 255, Whitealfa);
    }

    //黒のフェードイン
    public void BlackFadeIn()
    {
        Blackalfa += 0.3f * Time.deltaTime;
        BlackImage.color = new Color(0, 0, 0, Blackalfa);
    }
}
