using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LayerMask _mapLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    private float lineDis;

    PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        RaycastHit hit;
        _lineRenderer.SetPosition(0, transform.position);
        if(Physics.Raycast(transform.position, transform.forward, out hit, 25f, _mapLayerMask))
        {
            _lineRenderer.SetPosition(1, hit.point);
            lineDis = hit.distance;
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + transform.forward * 25f);
        }

        if (Physics.Raycast(transform.position, transform.forward, out hit, lineDis, _playerLayerMask))
        {
            Debug.LogError("�������� �÷��̾�� ����");
            _playerMovement.IsDead = true;
        }
    }
}
