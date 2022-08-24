using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private bool _playerIsHere;
    public bool playerIsHere {
        get => _playerIsHere;
        set {
            if(value)
               GameManager.CameraConfiner.m_BoundingShape2D = _c;
            _playerIsHere = value;
        }
    }
    
    public bool completed;
    public List<Condition> conditions;

    private PolygonCollider2D _c;

    // Start is called before the first frame update
    void Start()
    {
        _c = GetComponent<PolygonCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(col.GetComponent<Clone>().controlled)
                playerIsHere = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(col.GetComponent<Clone>().controlled)
                playerIsHere = true;
            else playerIsHere = false;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(col.GetComponent<Clone>().controlled)
                playerIsHere = false;
        }
    }
}
