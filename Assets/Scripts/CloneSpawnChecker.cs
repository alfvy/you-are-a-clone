using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpawnChecker : MonoBehaviour
{
    public bool canSpawn;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))
            canSpawn = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))
            canSpawn = false;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player"))
            canSpawn = true;
    }
}
