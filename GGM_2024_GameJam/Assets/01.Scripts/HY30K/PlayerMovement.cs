using Collections.Shaders.CircleTransition;
using DG.Tweening;
using System;
using System.Collections;
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
    [SerializeField] private GameObject _Crown;
    [SerializeField] float zPos = -2.08f;
    //[SerializeField] private ParticleSystem _deadParticle;

    public bool IsDead = false;
    public bool isOneDead = false;

    public int FacingDirection { get; private set; } = 1;

    protected bool _facingRight = true;
    private PlayerInput _playerInput;
    private Animator _animator;
    private FollowEnemy _followEnemy;

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

    private bool _onPlatform = false;

    public bool OnPlatform
    {
        get => _onPlatform;
        set => _onPlatform = value;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMovement += SetMovement;
        _playerInput.OnJump += Jump;
    }

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _followEnemy = FindObjectOfType<FollowEnemy>();

        if (_characterController == null)
            Debug.Log("�� ^�Ӥӹ�");
    }

    private void FixedUpdate()
    {
        if (IsDead && !isOneDead)
        {
            PlayerDead();
            isOneDead = true;
        }

        if (IsDead)
        {
            GameManager.Instance.CanConvert = true;
        }

        //?ï¿½ë³´?ï¿½ë¡œ ?ï¿½ì§??ï¿½ë§Œ ?ï¿½ë ?��???ï¿½ì§?´ï¿½?
        if (IsDead == false || _onPlatform == false)
        {
            if (_activeMove && GameManager.Instance.Is3D)
            {
                CalculatePlayerMovement();
            }
            else
            {
                CalulatePlayer2DMovement();
            }
            if (!GameManager.Instance.Is3D)
                ApplyGravity(); //ì¤?�ë �??ï¿½ìš© (2D?ï¿½ë?�Œ�??

            Move();
            AnimatorControl();
            PlayerRotate();
        }
        //float z = Math.Clamp(transform.position.z, -1.35f, -1.30f);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, zPos);
    }

    private void PlayerDead()
    {
        SoundManager.Instance?.PlaySFX("die");
        CircleTransition.Instance.CloseBlackScreen();
        _visual.gameObject.SetActive(false);
        _Crown.gameObject.SetActive(false);
        ResetPosition();

        Invoke("Deadfalse", 1f);
    }

    public void ResetPosition()
    {
        /*       Physics.gravity = Vector3.zero;
                transform.position = Vector3.zero;
                Debug.Log(GetComponent<CharacterController>().velocity);*/
        _characterController.enabled = false;
        /*     Debug.Log(transform.position);
             CharacterController a = gameObject.GetComponent<CharacterController>();
             a.Move(StageManager.Instance.StageValue[StageManager.Instance.CurrentStage].reStartPos.position);
             //GetComponent<CharacterController>().transform.position = StageManager.Instance.StageValue[StageManager.Instance.CurrentStage].reStartPos.position;
             Debug.Log(GetComponent<CharacterController>().velocity);*/
    }

    private void Deadfalse()
    {
        Debug.Log("1�� �ڿ�");
        transform.position = StageManager.Instance.StageValue[StageManager.Instance.CurrentStage].reStartPos.position;
        _characterController.enabled = true;
        _visual.gameObject.SetActive(true);
        _Crown.gameObject.SetActive(true);

        Invoke("DeadfalseInvoke", 1f);

        IsDead = false;
        isOneDead = false;
    }

    private void DeadfalseInvoke()
    {
        CircleTransition.Instance.OpenBlackScreen();
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

    public void SetMovement(Vector2 vector)
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
        {
            _visual.transform.rotation = Quaternion.Euler(0, 90, 0);
            _Crown.transform.rotation = Quaternion.Euler(0, 0, -15);
            _Crown.transform.position = transform.position + new Vector3(0.2f, 0.554f, 0);
        }
        else if (_inputDirection.x < 0)
        {
            _visual.transform.rotation = Quaternion.Euler(0, -90, 0);
            _Crown.transform.rotation = Quaternion.Euler(0, 0, 15);
            _Crown.transform.position = transform.position + new Vector3(-0.2f, 0.554f, 0);
        }
        else
        {
            _visual.transform.rotation = Quaternion.Euler(0, 180, 0);
            _Crown.transform.rotation = Quaternion.Euler(0, 0, 0);
            _Crown.transform.position = transform.position + new Vector3(0, 0.554f, 0);
        }
    }

    // ì¦?�ì‹�??ï¿½ï¿½?
    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
    }

    private void ApplyGravity()
    {
        if (IsGround && _verticalVelocity < 0)  //?ï¿½ì?��?ì°©ï¿½? ?ï¿½í?œ
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
