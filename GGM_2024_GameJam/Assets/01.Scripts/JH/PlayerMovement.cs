using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private CharacterController _characterController;
    private Rigidbody _rb;

    private float xInput;
    private float yInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");


        if (GameManager.Instance.Is3D == false)
        {
            transform.rotation = Quaternion.Euler(90, 90, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
