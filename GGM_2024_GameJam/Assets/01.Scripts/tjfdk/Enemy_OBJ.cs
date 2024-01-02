using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy_OBJ : MonoBehaviour
{
    Rigidbody rb;

    [Header("Move")]
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;

    private int idx = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 target = points[idx].position;
        Vector3 dir = (target - transform.position).normalized;
        rb.velocity = dir * speed;

        float distance = Vector3.Distance(transform.position, target);

        if (distance < 0.1f)
            idx = (idx + 1) % points.Length;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("test1"))
        {
            
        }
    }
}
