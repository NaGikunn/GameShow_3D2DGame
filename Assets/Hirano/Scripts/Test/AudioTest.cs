using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private AudioSource audio1;

	// Use this for initialization
	void Start ()
    {
        audio1 = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.O)|| Input.GetKeyDown(KeyCode.P))
        {
            audio1.Play();
        }
	}
}
