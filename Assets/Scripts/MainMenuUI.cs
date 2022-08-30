using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{


    private Animator _a;

    int Intro = Animator.StringToHash("Intro");
    int StartAndQuit = Animator.StringToHash("Start And Quit");
    int BringUpButtons = Animator.StringToHash("Bring Buttons Up");

    // Start is called before the first frame update
    void Start()
    {
        _a = GetComponent<Animator>();
        _a.CrossFade(Intro, 0, 0, 0, 0);
    }

    public void LateUpdate()
    {
        if(Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<GameStarter>().StartGame();
        }
    }

    public void Buttons(int i)
    {
        if(PlayerPrefs.GetInt(GameManager.PlayedBefore) == 1)
        {
            _a.CrossFade(BringUpButtons, 0, 0, 0, 0);
        }
        else
        {
            _a.CrossFade(StartAndQuit, 0, 0, 0, 0);
        }
    }
}
