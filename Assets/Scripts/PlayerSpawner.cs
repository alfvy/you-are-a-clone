using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject clonePrefab;
    public GameObject TAS;


    public void SpawnPlayer()
    {
        StartCoroutine(Spawn());
    }

    public void RespawnPlayer()
    {
        StartCoroutine(ReSpawnPlayerRoutine());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.25f);
        GameManager.CloneManager.AddClone(Instantiate(clonePrefab, transform.position, Quaternion.identity));
        Instantiate(TAS, transform.position, Quaternion.identity);
    }

    IEnumerator ReSpawnPlayerRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.CloneManager.AddClone(Instantiate(clonePrefab, transform.position, Quaternion.identity));
    }
}
