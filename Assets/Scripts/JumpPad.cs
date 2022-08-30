using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // simple jump pad script that makes the player jump when the player triggers the collider

    public float jumpSpeed = 5;

    private Animator _a;
    private AudioSource _as;

    int Activate = Animator.StringToHash("Activate");

    void Start()
    {
        _a = GetComponent<Animator>();
        _as = GetComponent<AudioSource>();
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _a.SetTrigger(Activate);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            _a.SetTrigger(Activate);
            _as.Play();
        }
    }
}
