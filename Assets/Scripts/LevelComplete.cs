using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public Level level;
    public Level nextLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !level.completed)
        {
            var clone = other.gameObject.GetComponent<Clone>();
            if (clone.controlled)
            {
                level.completed = true;
                GameManager.CloneManager.ChangeLevel(nextLevel);
                if (PlayerPrefs.GetInt(GameManager.Level) < level.number)
                {
                    PlayerPrefs.SetInt(GameManager.Level, level.number);
                    PlayerPrefs.Save();
                }
                clone.ResetPowerUps();
            }
        }    
    }
}
