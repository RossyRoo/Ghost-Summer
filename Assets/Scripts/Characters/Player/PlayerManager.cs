using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerLocomotionHandler _playerLocomotionHandler;
    private PlayerInteractionHandler _playerInteractionHandler;

    private void Awake()
    {
        _playerLocomotionHandler = GetComponent<PlayerLocomotionHandler>();
        _playerInteractionHandler = GetComponent<PlayerInteractionHandler>();
    }


    private void Update()
    {
        _playerInteractionHandler.CheckForInteractables();
    }

    private void FixedUpdate()
    {
        _playerLocomotionHandler.Move();
        _playerLocomotionHandler.HandleFaceDirection();
    }
}
