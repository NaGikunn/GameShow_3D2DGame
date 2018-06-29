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
        for(int s = 0; s <= 3; s++)
        {
            images[s].sprite = numberSprites[9];
        }
        StartCoroutine(col());

	}
	
	// Update is called once per frame
	void Update ()
    {

    }
    IEnumerator col()
    {
        for (int i = 0; i <= 9; i++)
        {
            images[0].sprite = numberSprites[i];
            if (i == 9)
            {
                i -= 10;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
