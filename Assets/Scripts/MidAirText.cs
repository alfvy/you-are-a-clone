using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MidAirText : MonoBehaviour
{
    public TextMeshPro text, text2;

    public bool extended;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && extended)
        {
            text.enabled = true;
        }

        if (other.gameObject.CompareTag("Player") && !text.enabled)
        {
            text2.text = "press R to spawn a clone mid air";
            extended = true;
        }   
    }
}
