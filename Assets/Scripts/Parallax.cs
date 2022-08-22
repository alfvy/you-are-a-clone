using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    private SpriteRenderer _renderer;
    public float parallaxAmount;
    private float _length, _startPos;
    private float _dist;
    
    private Vector2 _previousCamPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
        _renderer = GetComponent<SpriteRenderer>();
        _previousCamPos = cam.transform.position;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        _dist = (cam.transform.position.x * parallaxAmount);

        transform.position = new Vector2(_startPos + _dist, cam.transform.position.y);
        
        _previousCamPos = cam.transform.position;
    }
}