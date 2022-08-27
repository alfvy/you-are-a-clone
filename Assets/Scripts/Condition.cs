using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    [SerializeField] private bool _state;
    public bool state{
        get => _state;
        set {
            // if(level.conditions.TrueForAll(c=> c.state))
            //     level.completed = true;
            _state = value;
        }
    }
    public Level level;

    virtual public void SetState(bool state){}
}
