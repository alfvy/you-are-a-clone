using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        print(col.gameObject.name);
        if (col.gameObject.CompareTag("Player"))
        {
            var level = GameManager.currentLevel;
            if(GameManager.CloneManager.clones.Count == 1)
            {
                GameManager.CloneManager.RemoveClone(col.gameObject);
                Destroy(col.gameObject);
                level.playerSpawner.RespawnPlayer();
            } else if (GameManager.CloneManager.clones.Count > 1) {
                GameManager.CloneManager.RemoveClone(col.gameObject);
                Destroy(col.gameObject);
            }

            print("Died in Lava");
        }
    }
}
