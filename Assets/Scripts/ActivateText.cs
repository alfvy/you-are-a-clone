using System.Reflection.Emit;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateText : MonoBehaviour
{
    public TextMeshPro text;

    void Start()
    {
        text.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            text.enabled = true;
        }    
    }
}
