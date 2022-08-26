using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Level level;
    public float defDistanceRay = 100;
    public Transform firePoint;
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
            } else if (hit.collider.gameObject.CompareTag("Player") && GameManager.CloneManager.clones.Count > 1)
            {
                GameManager.CloneManager.RemoveClone(hit.collider.gameObject);
                Destroy(hit.collider.gameObject);
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, firePoint.position + firePoint.right * defDistanceRay);
        }
    }
}