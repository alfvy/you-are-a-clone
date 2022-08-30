using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Condition
{
    AudioSource _as;
    void Start() => _as = GetComponent<AudioSource>(); 

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !(other.gameObject.tag == "Ignore"))
        {
            other.GetComponent<Clone>().keys.Add(this);
            transform.position = new Vector2(1000, 1000);
            GameManager.keyCount++;
            _as.Play();
        }    
    }
}
