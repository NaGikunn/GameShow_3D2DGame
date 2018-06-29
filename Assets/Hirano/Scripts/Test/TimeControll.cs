using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeControll : MonoBehaviour
{
    [SerializeField]
    Image[] images = new Image[4];
    [SerializeField]
    Sprite[] numberSprites = new Sprite[11];
    public float TimeCount { get; private set; }

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
