using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBlock : MonoBehaviour, IReset
{
    Rigidbody rb;

    RigidbodyConstraints _2d = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    RigidbodyConstraints _3d = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    RigidbodyConstraints originFreezePos;
    RigidbodyConstraints originFreezeRot;

    private bool is3D;
    private Vector3 originPos;

    public void Reset()
    {
        transform.position = originPos;
        rb.constraints = originFreezePos | originFreezeRot;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        originPos = transform.position;
        originFreezePos 
            = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        originFreezeRot 
            = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

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
