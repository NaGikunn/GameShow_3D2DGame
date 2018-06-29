using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMove : MonoBehaviour
{
    Rigidbody rb;
    Vector3 Vec;
    bool big;
    Vector3 nowpos;
    Quaternion rotation;
    float speed = 2.0f;
    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        Vec = transform.position;
        big = false;
        nowpos = transform.position;
        rotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!big)
        {
            transform.position = new Vector3(Vec.x, Mathf.Sin(Time.time) * 0.5f + Vec.y, Vec.z);
        }
        else
        {
            if (rotation.y <= 360)
            {
                rotation.y += 4.0f * Time.deltaTime * speed;
                transform.rotation = new Quaternion(0,rotation.y * Time.deltaTime, 0,0);
                transform.localScale += new Vector3(0.009f, 0.009f, 0.009f) * Time.deltaTime;
            }
            if (nowpos.y <= 4)
            {
                nowpos.y += 0.5f * Time.deltaTime*speed;
                transform.position = nowpos;
            }
            
        }
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            big = true;
        }
    }
}
