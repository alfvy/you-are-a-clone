using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeleportingAudioSource : MonoBehaviour
{   
    Transform _t;

    void Start()
    {
        _t = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        try{
            _t.position = GameManager.CloneManager.clones.FirstOrDefault(c=> c.controlled).transform.position;
        } catch {}
    }

}
