using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy_OBJ : MonoBehaviour, IReset
{
    [Header("Ray")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int idx = 0;

    private bool isCollision = false;

    [Header("Reset")]
    [SerializeField] private Vector3 originPos;

    private void Awake()
    {
        originPos = transform.position;
    }

    public void Reset()
    {
        transform.position = originPos;
    }

    private void Update()
    {
        Ray();
        Move();
    }

    private void Ray()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, layer);

        if (colliders.Length > 0)
        {
            if (!isCollision)
            {
                isCollision = true;

                foreach (Collider collider in colliders)
                {
                    collider.transform.GetComponent<PlayerMovement>().IsDead = true;
                }
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

    private void Move()
    {
        Vector3 target = points[idx].position;
        Vector3 dir = (target - transform.position).normalized;
        transform.Translate(dir * speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, target);

        if (distance < 1f)
            idx = (idx + 1) % points.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
