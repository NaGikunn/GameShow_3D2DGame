using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chara : MonoBehaviour
{
    public static int Clear = 0;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Gole")
        {
            Clear++;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
