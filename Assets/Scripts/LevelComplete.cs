using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public Level level;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            level.completed = true;
        }    
    }
}
