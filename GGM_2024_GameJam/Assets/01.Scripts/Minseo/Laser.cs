using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;


    private void Update()
    {
        RaycastHit hit;
        _lineRenderer.SetPosition(0, transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 50f))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + transform.forward * 50f);
        }
    }
}
