using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActivateText : MonoBehaviour
{
    public TextMeshPro text;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !text.enabled)
        {
            text.enabled = true;
        }    
    }
}
