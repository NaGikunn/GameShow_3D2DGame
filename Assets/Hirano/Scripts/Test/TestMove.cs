using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour
{
    Rigidbody rb;
    Vector3 Vec;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Vec = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(Vec.x, Mathf.Sin(Time.time) * 0.5f + Vec.y, Vec.z);
    }

    void FixedUpdate()
    {
        
    }
}
