﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    
	// Use this for initialization
	void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        //SceaneManagerControllerを持ってくる
        SceaneManagerController.Instance.NowSceneManagement();
    }
}
