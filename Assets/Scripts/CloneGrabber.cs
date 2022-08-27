using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneGrabber : MonoBehaviour
{
    public bool grabbing;
    public GameObject grabbedClone;
    public Transform grabPos;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            var clone = other.gameObject.GetComponent<Clone>(); 
            if(clone.controlled) return;
            if(Input.GetKey(KeyCode.Q))
            {
                clone.SetGrabbed(grabPos, true);
                GetComponentInParent<Clone>().grabbing = true;
            }
        }    
    }
}
