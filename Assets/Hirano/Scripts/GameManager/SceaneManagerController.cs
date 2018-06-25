using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class SceaneManagerController : SingletonMonoBehaviour<SceaneManagerController>
{
    //現在のシーン名
    private string NowScene;
    //タイトルでの遷移フラグ
    private bool sceaneflg;
    //Time
    private float TIME = 0.0f;
    void Start()
    {
        TIME = 0.0f;
        sceaneflg = false;
    }

    public void NowSceneManagement()
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

            if (FadeManagerController.Whitealfa >= 0.0f)
            {
                FadeManagerController.Instance.WhiteFadeOut();
            }
            //すべてのボタン
            if (sceaneflg)
            {
                TIME += Time.deltaTime;
                if (FadeManagerController.Blackalfa <= 1.0f)
                {
                    FadeManagerController.Instance.BlackFadeIn();
                }
                if (AudioMnagerController.BGM.volume >= 0.0f)
                {
                    AudioMnagerController.Instance.BGMFadeOut();
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
            if (FadeManagerController.Blackalfa >= 0.0f)
            {
                FadeManagerController.Instance.BlackFadeOut();
            }
            if (PlayerMoveController.Clear)
            {
                FadeManagerController.Blackalfa = 0.0f;
                FadeManagerController.Instance.WhiteFadeOut();
            }
        }

        //チュートリアルステージなら
        if (NowScene == "StageTutorial_m")
        {
            //初めに黒のフェードアウト
            if (FadeManagerController.Blackalfa >= 0.0f)
            {
                FadeManagerController.Instance.BlackFadeOut();
            }

            //clear時
            if (PlayerMoveController.Clear)
            {
                if (FadeManagerController.Whitealfa <= 1.0f)
                {
                    FadeManagerController.Instance.WhiteFadeIn();
                }
            }
        }

        if (NowScene == "StageTutorial_m" && !PlayerMoveController.Clear)
        {
            FadeManagerController.Whitealfa = 0.0f;
            return;
        }
    }
}
