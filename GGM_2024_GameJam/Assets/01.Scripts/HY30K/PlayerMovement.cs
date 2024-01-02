using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _rootTrm;
    [SerializeField] private float _gravityMultiplier = 4f;

    private PlayerInput _playerInput;

    private CharacterController _characterController;
    public bool IsGround
    {
        get => _characterController.isGrounded;
    }

    private Vector2 _inputDirection;
    private Vector3 _movementVelocity;
    private float _verticalVelocity;

    public Vector3 MovementVelocity => _movementVelocity;

    private bool _activeMove = true;
    public bool ActiveMove
    {
        get => _activeMove;
        set => _activeMove = value;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMovement += SetMovement;
        _playerInput.OnJump += Jump;
    }

    private void FixedUpdate()
    {
        //키보드로 움직일때만 이렇게 움직이고
        if (_activeMove && GameManager.Instance.Is3D)
        {
            CalculatePlayerMovement();
        }
        else
        {
            CalulatePlayer2DMovement();
        }
        if (!GameManager.Instance.Is3D)
            ApplyGravity(); //중력 적용 (2D일때만)

        Move();
        PlayerRotate();
    }

    private void SetMovement(Vector2 vector)
    {
        _inputDirection = vector;
    }

    private void CalculatePlayerMovement()
    {
        _movementVelocity = (_rootTrm.forward * _inputDirection.y + _rootTrm.right * _inputDirection.x)
                            * (_moveSpeed * Time.fixedDeltaTime);
    }

    private void CalulatePlayer2DMovement()
    {
        _movementVelocity = (_rootTrm.right * _inputDirection.x) * (_moveSpeed * Time.fixedDeltaTime);
    }

    // 즉시 정지
    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0)  //땅에 착지 상태
        {
            _verticalVelocity = -1f;
        }
        else
        {
            _verticalVelocity += _gravity * _gravityMultiplier * Time.fixedDeltaTime;
        }

        _movementVelocity.y = _verticalVelocity;
    }

    private void Move()
    {
        _characterController.Move(_movementVelocity);
    }

    private void Jump()
    {
        if (!IsGround) return;
        if (!GameManager.Instance.Is3D)
            _verticalVelocity += _jumpPower;
    }

    private void PlayerRotate()
    {
        if (!GameManager.Instance.Is3D)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
    }
}
