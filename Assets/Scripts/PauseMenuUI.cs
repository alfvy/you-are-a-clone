using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource music;
    public float startVolume;
    public bool paused;

    void Start()
    {
        paused = false;
        startVolume = music.volume;
    }

    void LateUpdate()
    {
        if ((Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Escape)) && !paused && GameManager.CloneManager)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            StartCoroutine(GameManager.StartFade(music, 1f, startVolume - 0.4f));
        } else if ((Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Escape)) && paused && GameManager.CloneManager) {
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            StartCoroutine(GameManager.StartFade(music, 1f, startVolume));
        }
    }
}
