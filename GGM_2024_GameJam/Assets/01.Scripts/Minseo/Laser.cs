using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private LayerMask _mapLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;

    private float lineDis;

    PlayerMovement _playerMovement;
    [SerializeField] bool isCollision = false;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        //RaycastHit hit;
        //_lineRenderer.SetPosition(0, transform.position);
        //if (Physics.Raycast(transform.position, transform.forward, out hit, 25f, _mapLayerMask))
        //{
        //    _lineRenderer.SetPosition(1, hit.point);
        //    lineDis = hit.distance;
        //}
        //else
        //{
        //    _lineRenderer.SetPosition(1, transform.position + transform.forward * 25f);
        //}

        //Debug.DrawRay(transform.position, transform.forward * lineDis, Color.yellow);

        //if (Physics.Raycast(transform.position, transform.forward, out hit, lineDis, _playerLayerMask))
        //{
        //    Debug.LogError("·¹ÀÌÀú¶û ÇÃ·¹ÀÌ¾î¶û ´êÀ½");
        //    _playerMovement.IsDead = true;
        //}

        Ray();
    }
    
    private void Ray()
    {
        RaycastHit hit;
        _lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 25f, _mapLayerMask))
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
            if (!isCollision)
            {
                Debug.Log("µÚÁü");
                isCollision = true;
                if (GameManager.Instance.cannotAttack == false)
                    _playerMovement.IsDead = true;
            }
        }
        else
        {
            if (isCollision)
            {
                isCollision = false;
            }
        }
    }
}


