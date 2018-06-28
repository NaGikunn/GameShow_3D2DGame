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

    void Awake()
    {
        TIME = 0.0f;
        sceaneflg = false;
        FadeManagerController.Blackalfa = 0.0f;
        SelectStagePlayer.Load = false;
    }

    public void NowSceneManagement()
    {

        //現在のシーン
        NowScene = SceneManager.GetActiveScene().name;
        //タイトルなら
        if (NowScene == "Title")
        {
            //何かボタンを押したら
            if (Input.anyKeyDown)
            {
                sceaneflg = true;
            }

            //初めにWhiteImageのアルファを小さくする
            if (FadeManagerController.Whitealfa >= 0.0f)
            {
                FadeManagerController.Instance.WhiteFadeOut();
            }
            //ボタンを押したら
            if (sceaneflg)
            {
                TIME += Time.deltaTime;
                //BlackImageのアルファを多きくする
                if (FadeManagerController.Blackalfa <= 1.0f)
                {
                    FadeManagerController.Instance.BlackFadeIn();
                }
                //BGMを小さくする
                //if (AudioMnagerController.BGM1.volume >= 0.0f)
                //{
                //    AudioMnagerController.Instance.BGMFadeOut();
                //}
                //三秒たったら
                if (TIME >= 3.0f)
                {
                    //ステージセレクトへ
                    SceneManager.LoadScene("StageSelect");
                    TIME = 0.0f;
                }
            }
        }

        //セレクト画面なら
        if (NowScene == "StageSelect")
        {
            //初めにBlackImageのアルファを小さくする
            if (FadeManagerController.Blackalfa >= 0.0f)
            {
                FadeManagerController.Instance.BlackFadeOut();
            }
            //セレクト画面でプレイヤーがステージに触ったら
            if (SelectStagePlayer.Load)
            {
                StageSelectFade();
            }
            //クリアしているときにセレクト画面に来たら白のフェード
            if (PlayerMoveController.Clear)
            {
                FadeManagerController.Blackalfa = 0.0f;
                FadeManagerController.Instance.WhiteFadeOut();
            }
        }

        //チュートリアルステージなら
        if (NowScene == "StageTutorial_m")
        {
            //ここが何回も呼ばれてるよ
            AudioMnagerController.BGM2.Play();
            //初めにBlackImageのアルファを小さくする
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
    }

    public void StageSelectFade()
    {
        TIME += Time.deltaTime;
        if (FadeManagerController.Blackalfa <= 1.0f)
        {
            FadeManagerController.Instance.BlackFadeIn();
        }
        if (AudioMnagerController.BGM1.volume >= 0.0f)
        {
            AudioMnagerController.Instance.BGMFadeOut();
        }
        if (TIME >= 3.0f)
        {
            AudioMnagerController.BGM1.Pause();
            if(SelectStagePlayer.ObjectName == "StageTutorial")
            {
                SceneManager.LoadScene("StageTutorial_m");
            }
            if(SelectStagePlayer.ObjectName == "Stage1")
            {
                SceneManager.LoadScene("Stage1");
            }
            TIME = 0.0f;
        }
    }
}
