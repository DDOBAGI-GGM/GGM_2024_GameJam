using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravity_OBJ : MonoBehaviour
{
    private Rigidbody rb;
    public bool test = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GameManager.Instance.Is3D = !GameManager.Instance.Is3D;

        if (GameManager.Instance.Is3D)
            rb.useGravity = false;
        else
            rb.useGravity = true;
    }
}
