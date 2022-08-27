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
    public PlayerSpawner playerSpawner;
    public bool completed;
    public List<Condition> conditions;
    public Bool boolCondition;
    public int maxClones;
    public int number;

    private PolygonCollider2D _c;

    // Start is called before the first frame update
    void Start()
    {
        _c = GetComponent<PolygonCollider2D>();
    }

    void LateUpdate()
    {
        bool state;

        state = boolCondition == Bool.And;

        foreach(var condition in conditions)
        {
            switch(boolCondition)
            {
                case Bool.Or:
                state = state || condition.state;
                    break;
                case Bool.And:
                state = state && condition.state;
                    break;
            }
        }

        if(playerIsHere)
        {
            GameManager.currentLevel = this;
            GameManager.CloneManager.maxClones = maxClones;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            var clone = col.GetComponent<Clone>();
            if(clone.controlled)
            {
                playerIsHere = true;
                // GameManager.CloneManager.EnterLevel(this, maxClones, clone);
            }
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
