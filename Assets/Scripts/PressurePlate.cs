using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PressurePlate : Condition
{
    [SerializeField] Sprite activated, deactivated;
    [SerializeField] new Light2D light;

    private BoxCollider2D _c;
    private SpriteRenderer _s;

    void Start()
    {
        _c = GetComponent<BoxCollider2D>();
        _s = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _s.sprite = activated;
        light.enabled = true;
        state = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        _s.sprite = deactivated;
        light.enabled = false;
        state = false;    
    }
}
