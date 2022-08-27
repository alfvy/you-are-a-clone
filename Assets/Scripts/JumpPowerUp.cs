using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public bool used = false;

    // IEnumerator IncreaseCloneJumpHeight(Clone clone)
    // {
    //     var ogJump = clone.jumpSpeed;
    //     clone.jumpSpeed = 2 + ogJump;
    //     GetComponent<Animator>().CrossFade("Jump Empty", 0, 0, 0, 0);
    //     used = true;
    //     yield return new WaitForSeconds(20f);
    //     clone.jumpSpeed = ogJump;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !used)
        {
            // StartCoroutine(IncreaseCloneJumpHeight(other.gameObject.GetComponent<Clone>()));
            other.gameObject.GetComponent<Clone>().jumpPowerup = true;
            GetComponent<Animator>().CrossFade("Jump Empty", 0, 0, 0, 0);
            used = true;
        }    
    }

    internal void Reset()
    {
        GetComponent<Animator>().CrossFade("Jump", 0, 0, 0, 0);
        used = false;
    }
}
