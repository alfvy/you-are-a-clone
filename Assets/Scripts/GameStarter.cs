using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStarter : MonoBehaviour
{
    public bool debug;
    public int level;

    public PlayerSpawner playerSpawner;
    public GameObject[] uiObjects;
    public GameObject[] levelObjects;
    public GameObject cloneCounter, parallaxTrigger, gameMusic;
    public AudioClip death;
    public AudioListener al;
    public List<Level> levels;

    // Start is called before the first frame update
    void Start()
    {
        if(debug)
        {
            var cm = gameObject.AddComponent<CloneManager>();
                cm.maxClones = 1;
                cm.death = death;
            gameObject.AddComponent<GameManager>();
            foreach (GameObject uiObject in uiObjects)
            {
                uiObject.SetActive(false);
            }
            foreach (GameObject go in levelObjects)
            {
                go.SetActive(true);
            }
            cloneCounter.SetActive(true);
            levels.FirstOrDefault(l=> l.number == level).playerSpawner.SpawnPlayer();
            parallaxTrigger.SetActive(true);
            PlayerPrefs.SetInt(GameManager.PlayedBefore, 1);
            PlayerPrefs.Save();
            gameMusic.SetActive(true);
            Destroy(al);
            Destroy(this);
        }

        foreach (GameObject go in levelObjects)
        {
            go.SetActive(false);
        }
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
        foreach (GameObject go in levelObjects)
        {
            go.SetActive(true);
        }
        cloneCounter.SetActive(true);
        parallaxTrigger.SetActive(true);
        PlayerPrefs.SetInt(GameManager.PlayedBefore, 1);
        PlayerPrefs.Save();
        gameMusic.SetActive(true);
        playerSpawner.SpawnPlayer();
        // GameManager.VirtualCamera.Follow = playerSpawner.transform;
        Destroy(al);
        Destroy(this);
    }

    public void ContinueGame()
    {
        Level loadLevel;
        CloneManager cm;
        try {
            loadLevel = levels.FirstOrDefault(l=>l.number==PlayerPrefs.GetInt(GameManager.Level));
            cm = gameObject.AddComponent<CloneManager>();
                cm.maxClones = loadLevel.maxClones;
                cm.death = death;
            gameObject.AddComponent<GameManager>();
            foreach (GameObject uiObject in uiObjects)
            {
                uiObject.SetActive(false);
            }
            foreach (GameObject go in levelObjects)
            {
                go.SetActive(true);
            }
            cloneCounter.SetActive(true);
            parallaxTrigger.SetActive(true);
            gameMusic.SetActive(true);
            // GameManager.VirtualCamera.Follow = loadLevel.playerSpawner.transform;
            loadLevel.playerSpawner.SpawnPlayer();
            Destroy(al);
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
            foreach (GameObject go in levelObjects)
            {
                go.SetActive(true);
            }
            cloneCounter.SetActive(true);
            parallaxTrigger.SetActive(true);
            gameMusic.SetActive(true);
            // GameManager.VirtualCamera.Follow = playerSpawner.transform;
            playerSpawner.SpawnPlayer();
            Destroy(al);
            Destroy(this);
        }
    }
}
