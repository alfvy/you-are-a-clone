using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Condition condition;

    private Animator _a;
    private BoxCollider2D _c;
    int Active = Animator.StringToHash("Active");

    void Start()
    {
        _a = GetComponent<Animator>();
        _c = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _a.SetBool(Active, condition.state);
    }

    public void DisableCollision(int set)
    {
        _c.enabled = set == 1;
    }
}
