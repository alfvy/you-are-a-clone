using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxUI : MonoBehaviour
{
    public float speed = 20;
    private RectTransform _t;

    void Start()
    {
        _t = GetComponent<RectTransform>();
    }

    void Update()
    {
        _t.anchoredPosition += new Vector2(speed * Time.unscaledDeltaTime, 0);
    }
}
