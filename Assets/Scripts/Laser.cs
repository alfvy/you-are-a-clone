using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Level level;
    public Bool boolCondition;
    public List<Condition> conditions;
    public float defDistanceRay = 100;
    public Transform firePoint;
    private bool startActivated;
    private LineRenderer _lineRenderer;

    // draw a line from the fire point to a hit on a raycast starting from the fire point
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, defDistanceRay, ~LayerMask.GetMask("Level"));
        _lineRenderer.SetPosition(0, firePoint.position);
        if (hit.collider != null)
        {
            _lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.gameObject.CompareTag("Player") && GameManager.CloneManager.clones.Count == 1)
            {
                GameManager.CloneManager.RemoveClone(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
                level.playerSpawner.RespawnPlayer();
                level.Reset();
                print("Died from a lazer");
            } else if (hit.collider.gameObject.CompareTag("Player") && GameManager.CloneManager.clones.Count > 1)
            {
                GameManager.CloneManager.RemoveClone(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
                print("Died from a lazer");
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, firePoint.position + firePoint.right * defDistanceRay);
        }

        var state = boolCondition == Bool.And;

        foreach(var condition in conditions)
        {
            switch(boolCondition)
            {
                case Bool.Or:
                state = state || condition.state;
                    break;
                case Bool.And:
                if(startActivated)
                    state = !state && condition.state;
                else state = state && condition.state;
                    break;
            }
        }
    }
}