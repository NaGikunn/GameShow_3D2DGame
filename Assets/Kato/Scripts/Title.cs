﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // ワールドのy軸に沿って1秒間に90度回転
        transform.Rotate(new Vector3(0, 30,0) * Time.deltaTime, Space.World);
    }
}