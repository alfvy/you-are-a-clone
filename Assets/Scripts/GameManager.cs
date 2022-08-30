using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using CommandTerminal;
public class GameManager : MonoBehaviour
{
    public Level sLevel;
    public static Level currentLevel;

    public static CinemachineVirtualCamera VirtualCamera;
    public static CinemachineConfiner2D CameraConfiner;
    public static CloneManager CloneManager;

    void Start()
    {
        playTime = 0;
        VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        CameraConfiner = GameObject.FindObjectOfType<CinemachineConfiner2D>();
        CloneManager = GetComponent<CloneManager>();
    }

    void Reset()
    {
        VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        CameraConfiner = GameObject.FindObjectOfType<CinemachineConfiner2D>();
        CloneManager = GameObject.FindObjectOfType<CloneManager>();
    }

    void LateUpdate()
    {
        sLevel = currentLevel;
        playTime += Time.deltaTime;
        // if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    [RegisterCommand(Name = "player_prefs", Help = "Prints the value of  a player pref", MinArgCount = 2, MaxArgCount = 2)]
    public static void PlayerPrefsChecker(CommandArg[] args)
    {
        string playerPref = args[0].String;
        int type = args[1].Int;

        if(Terminal.IssuedError) return;

        switch(type)
        {
            case 0:
                Terminal.Log(PlayerPrefs.GetInt(playerPref).ToString());
                break;
            case 1:
                Terminal.Log(PlayerPrefs.GetFloat(playerPref).ToString());
                break;
            case 2:
                Terminal.Log(PlayerPrefs.GetString(playerPref).ToString());
                break;
        }
        // Terminal.print(PlayerPrefs.GetInt(args[0].ToString()).ToString());
    }

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static string PlayedBefore = "PlayedBefore";
    public static string Level = "Level";
    public static string PlayTime = "PlayTime";
    public static string JumpCount = "JumpCount";
    public static string KeyCount = "KeyCount";

    public static float playTime;
    public static int jumpCount;
    public static int keyCount; 
    public static int deathCount;
    public static int cloneCount;
}
