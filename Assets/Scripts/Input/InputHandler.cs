using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    PlayerInteractionHandler _playerInteractionHandler;

    public Vector2 CurrentMoveInput { get; set; }
    public Vector2 LastMoveInput { get; set;}

    private void Awake()
    {
        _playerInteractionHandler = FindObjectOfType<PlayerInteractionHandler>();
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CurrentMoveInput = context.ReadValue<Vector2>();
            LastMoveInput = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            CurrentMoveInput = Vector2.zero;
        }
    }

    public void HandleInteractInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            _playerInteractionHandler.TryInteraction();
        }
    }
}
