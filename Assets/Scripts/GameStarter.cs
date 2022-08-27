using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStarter : MonoBehaviour
{
    public bool debug;

    public PlayerSpawner playerSpawner;
    public GameObject[] uiObjects;
    public GameObject cloneCounter;
    public GameObject parallaxTrigger;
    public AudioClip death;

    public List<Level> levels;

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
        levels = levels.OrderBy(l => l.number).ToList();
        // PlayerPrefs.SetInt(GameManager.PlayedBefore, 0);
        // PlayerPrefs.SetInt(GameManager.Level, 0);
        // PlayerPrefs.SetFloat(GameManager.PlayTime, 0);
        // PlayerPrefs.SetInt(GameManager.JumpCount, 0);
        // PlayerPrefs.Save();
    }

    public void StartGame()
    {
        var cm = gameObject.AddComponent<CloneManager>();
            cm.maxClones = 1;
            cm.death = death;
        gameObject.AddComponent<GameManager>();
        foreach (GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(false);
        }
        cloneCounter.SetActive(true);
        playerSpawner.SpawnPlayer();
        parallaxTrigger.SetActive(true);
        PlayerPrefs.SetInt(GameManager.PlayedBefore, 1);
        PlayerPrefs.Save();
        Destroy(this);
    }

    public void ContinueGame()
    {
        Level loadLevel;
        CloneManager cm;
        try {
            loadLevel = levels[PlayerPrefs.GetInt(GameManager.Level)];
            cm = gameObject.AddComponent<CloneManager>();
                cm.maxClones = loadLevel.maxClones;
                cm.death = death;
            gameObject.AddComponent<GameManager>();
            foreach (GameObject uiObject in uiObjects)
            {
                uiObject.SetActive(false);
            }
            cloneCounter.SetActive(true);
            loadLevel.playerSpawner.SpawnPlayer();
            parallaxTrigger.SetActive(true);
            Destroy(this);
        } catch {
            cm = gameObject.AddComponent<CloneManager>();
                cm.maxClones = 1;
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
}
