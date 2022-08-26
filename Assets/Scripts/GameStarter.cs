using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public bool debug;

    public PlayerSpawner playerSpawner;
    public GameObject[] uiObjects;
    public GameObject cloneCounter;
    public GameObject parallaxTrigger;
    public AudioClip death;

    // Start is called before the first frame update
    void Start()
    {
        if(debug)
        {
            var cm = gameObject.AddComponent<CloneManager>();
                cm.maxClones = 6;
                cm.death = death;
            gameObject.AddComponent<GameManager>();
            foreach (GameObject uiObject in uiObjects)
            {
                uiObject.SetActive(false);
            }
            cloneCounter.SetActive(true);
            playerSpawner.SpawnPlayer();
            parallaxTrigger.SetActive(true);
            Destroy(this);
        }
    }

    public void StartGame()
    {
        var cm = gameObject.AddComponent<CloneManager>();
            cm.maxClones = 6;
            cm.death = death;
        gameObject.AddComponent<GameManager>();
        foreach (GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(false);
        }
        cloneCounter.SetActive(true);
        playerSpawner.SpawnPlayer();
        parallaxTrigger.SetActive(true);
        Destroy(this);
    }
}
