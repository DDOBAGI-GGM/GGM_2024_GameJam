using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_OBJ : MonoBehaviour, IReset
{
    Rigidbody rb;

    [Header("Ray")]
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    private int idx = 0;

    private Transform originPos;
    private bool isCollision = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originPos = transform;
    }

    public void Reset()
    {
        transform.position = originPos.position;
        isCollision = false;
        rb.velocity = Vector3.zero;
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
        rb.velocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target);

        if (distance < 0.1f)
            idx = (idx + 1) % points.Length;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
