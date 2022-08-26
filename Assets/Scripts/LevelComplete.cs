using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public Level level;
    public Level nextLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Clone>().controlled)
            {
                level.completed = true;
                GameManager.CloneManager.ChangeLevel(nextLevel);
            }
        }    
    }
}
