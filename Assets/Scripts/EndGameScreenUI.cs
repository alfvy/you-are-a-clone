using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class EndGameScreenUI : MonoBehaviour
{
    public GameObject endScreen, mountain;
    public TextMeshProUGUI playTime, Jumps, keys, thanks, credits, credits2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FadeInUI());
            StartCoroutine(UpdateValues());
        }    
    }

    private IEnumerator FadeInUI()
    {
        endScreen.SetActive(true);
        var endScreenImage = endScreen.GetComponent<Image>();
        endScreenImage.color = new Color(0, 0, 0, 0);
        mountain.SetActive(true);
        var mountainImage = mountain.GetComponent<Image>();
        mountainImage.color = new Color(1, 1, 1, 0);
        while (endScreenImage.color.a < 0.5f)
        {
            endScreenImage.color += new Color(0f, 0f, 0f, 0.1f);
            mountainImage.color += new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator UpdateValues()
    {
        playTime.text = FormatTime(Time.time);
        yield return new WaitForSeconds(1f);
        Jumps.text = GameManager.jumpCount.ToString();
        yield return new WaitForSeconds(1f);
        keys.text = GameManager.keyCount.ToString() + " / 3";
        yield return new WaitForSeconds(1f);
        thanks.text = "Thanks for playing this very short and buggy game";
        yield return new WaitForSeconds(1f);
        credits.text = "Programming, Music, Level Design done by alfy";
        yield return new WaitForSeconds(1f);
        credits2.text = "Art, Level Design done by BirdBut";
    }

    public string FormatTime( float time )
    {
        return TimeSpan.FromSeconds(Time.timeSinceLevelLoad).ToString();
    }
}
