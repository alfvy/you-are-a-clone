using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LayerCheck : MonoBehaviour
{
    public bool ground, oneWayGround;
    [SerializeField] AudioClip fall;

    private static int groundMask, oneWayGroundMask, waterMask, playerMask;
    private Collider2D _c;
    private Transform _t;

    // Start is called before the first frame update
    void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
        playerMask = LayerMask.GetMask("Player");
        // oneWayGroundMask = LayerMask.GetMask("OneWayGround");
        waterMask = LayerMask.GetMask("Water");
        _c = GetComponent<Collider2D>();
        _t = GetComponent<Transform>();
    }

    void Reset()
    {
        _c = GetComponent<Collider2D>();    
    }    

    // void LateUpdate()
    // {   
    //     var hit = Physics2D.Raycast(new Vector2(_t.position.x, _t.position.y - 0.25f), Vector2.up, 0.5f, waterMask);
    // }

    // void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.25f), Vector2.up * 0.5f);        
    // }

    void OnTriggerStay2D(Collider2D col)
    {
        ground = _c.IsTouchingLayers(groundMask | playerMask)
        || col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Player");
        // oneWayGround = _c.IsTouchingLayers(oneWayGroundMask) || col.gameObject.CompareTag("OneWayGround");
        // water = _c.IsTouchingLayers(waterMask) || col.gameObject.CompareTag("Water") || col.gameObject.layer == waterMask;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        ground = false;
        oneWayGround = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ground"))
            AudioSource.PlayClipAtPoint(fall, transform.position, 0.5f);
    }
}
