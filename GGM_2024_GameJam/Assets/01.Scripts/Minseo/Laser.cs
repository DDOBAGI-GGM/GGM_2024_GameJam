using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LayerMask _mapLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        RaycastHit hit;
        _lineRenderer.SetPosition(0, transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 50f, _mapLayerMask))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + transform.forward * 50f);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, 50f, _playerLayerMask))
        {
            _playerMovement.IsDead = true;
        }
    }
}
