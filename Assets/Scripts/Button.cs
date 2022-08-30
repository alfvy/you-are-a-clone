using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Button : Condition
{
    [SerializeField] Sprite activated, deactivated;
    [SerializeField] new Light2D light;
    [SerializeField] AudioClip click, lowClick;

    private BoxCollider2D _c;
    private SpriteRenderer _s;
    private AudioSource _as;
    private static int PlayerMask;

    void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");
        _c = GetComponent<BoxCollider2D>();
        _s = GetComponent<SpriteRenderer>();
        light = GetComponentInChildren<Light2D>();
        _as = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if (_c.IsTouchingLayers(PlayerMask) && (Input.GetKeyDown(KeyCode.E)  || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetButtonDown("Fire1")) && !state) {
            _s.sprite = activated;
            light.enabled = true;
            state = true;
            _as.PlayOneShot(click);
        } else if (_c.IsTouchingLayers(PlayerMask) && (Input.GetKeyDown(KeyCode.E)  || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetButtonDown("Fire1")) && state) {
            _s.sprite = deactivated;
            light.enabled = false;
            state = false;
            _as.PlayOneShot(lowClick);
        }        
    }

    public override void SetState(bool state)
    {
        this.state = state;
        _s.sprite = state ? activated : deactivated;
        light.enabled = state;
    }

    void OnTriggerStay2D(Collider2D other)
    {
    }
}
