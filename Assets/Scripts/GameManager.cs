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
        VirtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        CameraConfiner = GameObject.FindObjectOfType<CinemachineConfiner2D>();
        CloneManager = GetComponent<CloneManager>();
        PlayerPrefs.SetInt(PlayedBefore, 0);
        PlayerPrefs.SetInt(Level, 0);
        PlayerPrefs.SetFloat(PlayTime, 0);
        PlayerPrefs.SetInt(JumpCount, 0);
        PlayerPrefs.Save();
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

    public static string PlayedBefore = "PlayedBefore";
    public static string Level = "Level";
    public static string PlayTime = "PlayTime";
    public static string JumpCount = "JumpCount";
}
