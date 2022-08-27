using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Bool{
    Or, And
}

public class SlidingDoor : MonoBehaviour
{
    [SerializeField] AudioClip open, close;
    public Condition[] conditions;
    public Bool boolCondition;
    public bool startingState;

    private Animator _a;
    private BoxCollider2D _c;
    private AudioSource _as;
    int Active = Animator.StringToHash("Active");

    void Start()
    {
        _a = GetComponent<Animator>();
        _c = GetComponent<BoxCollider2D>();
        _as = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
                if (!startingState)
                    state = state && condition.state;
                else state = state && !condition.state;
                break;
            }
        }

        _a.SetBool(Active, state);
    }

    public void OpenDoorSound() => _as.PlayOneShot(open);
    public void CloseDoorSound() => _as.PlayOneShot(close);

    void OnCollisionEnter2D(Collision2D other)
    {
        // if(other.gameObject.tag == "Player" && needsKey)
        // {
        //     var playerKeys = other.gameObject.GetComponent<Clone>().keys;
        //     if(playerKeys.Any(k=> key.Equals(k)))
        //     {
        //         _a.SetBool(Active, true);
        //     }
        // }    
    }

    public void DisableCollision(int set)
    {
        _c.enabled = set == 1;
    }
}
