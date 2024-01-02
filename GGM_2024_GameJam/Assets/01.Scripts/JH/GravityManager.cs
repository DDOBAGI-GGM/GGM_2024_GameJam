using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance;

    public bool Is3D = false;

    private Vector3 _2DGravity = new Vector3(0, 0, -9.8f);
    private Vector3 _3DGravity = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Physics.gravity = _2DGravity;
    }

    void Update()
    {
        GravityConvert();
    }

    private void GravityConvert()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Is3D = !Is3D;

            if (Is3D == false)
            {
                Physics.gravity = _2DGravity;
            }
            else
            {
                Physics.gravity = _3DGravity;
            }
        }
    }
}
