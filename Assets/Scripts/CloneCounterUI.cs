using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneCounterUI : MonoBehaviour
{
    CloneManager _cloneManager;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        _cloneManager = GameManager.CloneManager;
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (float)_cloneManager.clones.Count / (float)_cloneManager.maxClones;
    }
}
