using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.InputSystem.InputAction;

public class DefaultInputActionEvents : MonoBehaviour
{
    [SerializeField] private InputActionMap actionMap;

    public InputAction movement, look, jump, crouch, run;

    //public CallbackContext movementContext, lookContext;

    [SerializeField] private PlayerInputManager playerInputManager;

    public void Initialize()
    {
        playerInputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>();
    }

    public void RegisterInputActions(InputActionMap actionMap)
    {
        movement = actionMap.FindAction("Movement");
        movement.performed += OnMovementAction;
        movement.canceled += OnMovementAction;
        look = actionMap.FindAction("Look");
        look.started += OnLookAction;
        look.canceled += OnLookAction;
        jump = actionMap.FindAction("Jump");
        jump.started += OnJumpAction;
        jump.canceled += OnJumpAction;
        crouch = actionMap.FindAction("Crouch");
        crouch.started += OnCrouchStart;
        crouch.canceled += OnCrouchCancel;
        run = actionMap.FindAction("Run");
        run.started += OnRunStart;
        run.canceled += OnRunCancel;
    }

    private void OnMovementAction(InputAction.CallbackContext context)
    {
        playerInputManager.Move(context);
    }

    private void OnLookAction(InputAction.CallbackContext context)
    {
        playerInputManager.Look(context);
    }

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        playerInputManager.Jump();
    }

    private void OnCrouchStart(InputAction.CallbackContext context)
    {
        playerInputManager.Crouch(true);
    }

    private void OnCrouchCancel(InputAction.CallbackContext context)
    {
        playerInputManager.Crouch(false);
    }

    private void OnRunStart(InputAction.CallbackContext context)
    {
        playerInputManager.SetRun(context);
    }

    private void OnRunCancel(InputAction.CallbackContext context)
    {
        playerInputManager.SetWalk(context);
    }
}
