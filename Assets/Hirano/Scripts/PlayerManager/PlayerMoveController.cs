using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class PlayerMoveController : MonoBehaviour
{
    protected PlayerManagerController Manager;
    protected Vector3 playerposition;
    protected Animator anim;
    protected Rigidbody rig;
    protected float flap = 225.0f;
    protected bool Jump = false;
    public static int Clear = 0;
    protected AudioSource SEAudio1;
    protected AudioSource SEAudio2;
    void Awake()
    {
        playerposition = transform.position;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        AudioSource[] audiosorce = GetComponents<AudioSource>();
        SEAudio1 = audiosorce[0];
        SEAudio2 = audiosorce[1];
        Manager = GetComponent<PlayerManagerController>();
        Clear = 0;
        PositionInitialization();
    }

    public abstract void PositionInitialization();
    public abstract void Movement();

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Stage"))
        {
            Jump = false;
            anim.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Gole"))
        {
            Clear += 1;
            Manager.ClearLabel.SetActive(true);
            Invoke("StageSlect", 2.0f);
        }
    }
 
     void StageSlect()
    {
        SceneManager.LoadScene("StageSelect");
    }
}
