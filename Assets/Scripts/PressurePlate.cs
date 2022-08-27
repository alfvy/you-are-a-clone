using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PressurePlate : Condition
{
    [SerializeField] Sprite activated, deactivated;
    [SerializeField] new Light2D light;
    [SerializeField] AudioClip click, lowClick;

    private BoxCollider2D _c;
    private SpriteRenderer _s;
    private AudioSource _as;

    void Start()
    {
        _c = GetComponent<BoxCollider2D>();
        _s = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
        _as = GetComponent<AudioSource>();
    }

    public override void SetState(bool state)
    {
        this.state = state;
        _s.sprite = state ? activated : deactivated;
        light.enabled = state;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _s.sprite = activated;
            light.enabled = true;
            state = true;
            _as.PlayOneShot(click);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _s.sprite = activated;
            light.enabled = true;
            state = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            _s.sprite = deactivated;
            light.enabled = false;
            state = false;    
            _as.PlayOneShot(lowClick);
        }
    }
}
