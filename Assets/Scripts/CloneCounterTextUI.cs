using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CloneCounterTextUI : MonoBehaviour
{
    // show the number of clones
    public TextMeshProUGUI text;
    
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"{GameManager.CloneManager.clones.Count} / {GameManager.CloneManager.maxClones}";
    }

}
