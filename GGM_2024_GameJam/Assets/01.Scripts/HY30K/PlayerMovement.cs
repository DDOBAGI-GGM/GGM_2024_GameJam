using DG.Tweening;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Transform _rootTrm;
    [SerializeField] private float _gravityMultiplier = 4f;
    [SerializeField] private GameObject _visual;
    [SerializeField] float zPos = -2.08f;

    public bool IsDead = false;
    public int FacingDirection { get; private set; } = 1;

    protected bool _facingRight = true;
    private PlayerInput _playerInput;
    private Animator _animator;

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
        _animator = gameObject.GetComponentInChildren<Animator>();
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMovement += SetMovement;
        _playerInput.OnJump += Jump;
    }

    private void FixedUpdate()
    {
        if (IsDead)
        {
            PlayerDead();
        }

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
        AnimatorControl();
        PlayerRotate();

        //float z = Math.Clamp(transform.position.z, -1.35f, -1.30f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, zPos);
    }

    private void PlayerDead()
    {
        StopImmediately();
        StageManager.Instance.ReSet();
    }

    private void AnimatorControl()
    {
        if (GameManager.Instance.Is3D)
        {
            if (_inputDirection.y > 0 || _inputDirection.x > 0 || _inputDirection.y < 0 || _inputDirection.x < 0)
                _animator.SetBool("IsMove", true);
            else
                _animator.SetBool("IsMove", false);
        }
        else
        {
            if (_inputDirection.x > 0 || _inputDirection.x < 0)
                _animator.SetBool("IsMove", true);
            else
                _animator.SetBool("IsMove", false);
        }
    }

    private void SetMovement(Vector2 vector)
    {
        _inputDirection = vector;
    }

    private void CalculatePlayerMovement()
    {
        _movementVelocity = (_rootTrm.forward * _inputDirection.y + _rootTrm.right * _inputDirection.x)
                        * (_moveSpeed * Time.fixedDeltaTime);

        // If there is movement, rotate the _visual object to face the movement direction
        if (_movementVelocity.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementVelocity.normalized, Vector3.back);
            _visual.transform.rotation = Quaternion.Slerp(_visual.transform.rotation, targetRotation, _rotateSpeed * Time.fixedDeltaTime);
        }
    }

    private void CalulatePlayer2DMovement()
    {
        _movementVelocity = (_rootTrm.right * _inputDirection.x) * (_moveSpeed * Time.fixedDeltaTime);

        if (_inputDirection.x > 0)
            _visual.transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (_inputDirection.x < 0)
            _visual.transform.rotation = Quaternion.Euler(0, -90, 0);
        else
            _visual.transform.rotation = Quaternion.Euler(0, 180, 0);
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
            _verticalVelocity = -0.1f;
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
        {
            _verticalVelocity += _jumpPower;
        }
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
