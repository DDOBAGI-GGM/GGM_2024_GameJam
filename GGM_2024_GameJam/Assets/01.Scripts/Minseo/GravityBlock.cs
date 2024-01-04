using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : MonoBehaviour
{
    Rigidbody rb;

    RigidbodyConstraints _2d = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    RigidbodyConstraints _3d = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

    private bool is3D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        is3D = !GameManager.Instance.Is3D;
    }

    private void Update()
    {
        if (GameManager.Instance.Is3D != is3D)
        {
            is3D = GameManager.Instance.Is3D;
            if (GameManager.Instance.Is3D)
            {
                rb.constraints = ~_2d;
                rb.constraints = _3d;
            }
            else
            {
                rb.constraints = ~_3d;
                rb.constraints = _2d;
            }
        }

        if (rb.velocity != Vector3.zero && GameManager.Instance.Is3D)
            rb.velocity = Vector3.zero;
    }
}
