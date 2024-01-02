using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _inputAction;
    public PlayerInputAction InputAction => _inputAction;

    public event Action<Vector2> OnMovement;
    public event Action OnJump;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();

        _inputAction.Player.Enable();
        _inputAction.Player.Jump.performed += JumpHandle;
    }

    private void JumpHandle(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
    private void Update()
    {
        Vector2 move = _inputAction.Player.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(move);
    }
}
