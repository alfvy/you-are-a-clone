using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public bool used = false;

    // IEnumerator SpeedUpClone(Clone clone)
    // {
    //     var ogSpeed = clone.walkSpeed;
    //     clone.walkSpeed = 3 + ogSpeed;
    //     GetComponent<Animator>().CrossFade("Speed Empty", 0, 0, 0, 0);
    //     used = true;
    //     yield return new WaitForSeconds(20f);
    //     clone.walkSpeed = ogSpeed;
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !used)
        {
            // StartCoroutine(SpeedUpClone(other.gameObject.GetComponent<Clone>()));
            other.gameObject.GetComponent<Clone>().speedPowerUp = true;
            GetComponent<Animator>().CrossFade("Speed Empty", 0, 0, 0, 0);
            used = true;
        }    
    }

    internal void Reset()
    {
        GetComponent<Animator>().CrossFade("Speed", 0, 0, 0, 0);
        used = false;
    }
}
