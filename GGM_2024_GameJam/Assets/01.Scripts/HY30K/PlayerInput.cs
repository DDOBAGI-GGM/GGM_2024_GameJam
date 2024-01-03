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

    private Vector2 move;
    public Vector2 Move { get { return move; } private set { } }

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
        Vector2 movement = _inputAction.Player.Movement.ReadValue<Vector2>();
        if (movement.x > 0) move.x = 1;
        else if (movement.x < 0) move.x = -1;
        if (movement.y > 0) move.y = 1;
        else if (movement.y < 0) move.y = -1;
        if (movement.x == 0 && movement.y != 0) move.x = 0;
        if (movement.x != 0 && movement.y == 0) move.y = 0;
        OnMovement?.Invoke(movement);
    }
}
