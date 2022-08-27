using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PermenantPressurePlate : Condition
{
    [SerializeField] Sprite activated, deactivated;
    [SerializeField] new Light2D light;

    private BoxCollider2D _c;
    private SpriteRenderer _s;
    private AudioSource _as;

    void Start()
    {
        _c = GetComponent<BoxCollider2D>();
        _s = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
        _as = GetComponent<AudioSource>();
        state = false;
    }

    public override void SetState(bool state)
    {
        this.state = state;
        _s.sprite = state ? activated : deactivated;
        light.enabled = state;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !state)
        {
            _s.sprite = deactivated;
            light.enabled = true;
            _as.Play();
            state = true;
        }
    }
}

