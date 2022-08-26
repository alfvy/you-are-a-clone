using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoyEnemy : MonoBehaviour
{
    public int direction;
    public float speed;
    private Rigidbody2D _rb;
    private Transform _t;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _t = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        _t.localScale = new Vector2(direction * 1f, 1f);
        _rb.velocity = new Vector2(speed * direction, _rb.velocity.y);
        // change animator state based on velocity
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // collision.gameObject.GetComponent<CLone>().Damage(1);
        }
    }

    // spin the enemy around once hitting a wall
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            direction *= -1;
            // _a.SetBool("isSpinning", true);
        }
    }

}
