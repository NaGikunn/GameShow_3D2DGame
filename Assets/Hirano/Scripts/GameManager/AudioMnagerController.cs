using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMnagerController : SingletonMonoBehaviour<AudioMnagerController>
{
    public static AudioSource BGM;

    // Use this for initialization
    void Start ()
    {
        BGM = GetComponent<AudioSource>();
    }

    public void BGMFadeOut()
    {
        BGM.volume -= 0.3f * Time.deltaTime;
    }
}
