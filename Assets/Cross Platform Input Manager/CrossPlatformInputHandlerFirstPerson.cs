using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class CrossPlatformInputHandlerFirstPerson : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private PlayerInput playerInputComponent = null;

    [SerializeField] private DefaultInputActionEvents actionEvents;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInputComponent == null)
        {
            playerInputComponent = gameObject.AddComponent<PlayerInput>();
            playerInputComponent.actions = inputActionAsset;
            playerInputComponent.neverAutoSwitchControlSchemes = true;
            playerInputComponent.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            playerInputComponent.currentActionMap = inputActionAsset.FindActionMap("FirstPerson");
        }
        else
        {
            playerInputComponent = GetComponent<PlayerInput>();
            playerInputComponent.actions = inputActionAsset;
            playerInputComponent.neverAutoSwitchControlSchemes = true;
            playerInputComponent.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            playerInputComponent.currentActionMap = inputActionAsset.FindActionMap("FirstPerson");
        }

        actionEvents = gameObject.AddComponent<DefaultInputActionEvents>();
        actionEvents.RegisterInputActions(playerInputComponent.currentActionMap);
        actionEvents.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionEvents.movement.phase == InputActionPhase.Started)
        {
            //Debug.Log(actionEvents.movementContext);
        }

        if (actionEvents.look.phase == InputActionPhase.Started)
        {
            //Debug.Log(actionEvents.lookContext);
        }
    }

    public void SetInputDevice(CrossPlatformInputManager.inputDevice device)
    {
        if (device == CrossPlatformInputManager.inputDevice.unknown)
        {
            playerInputComponent.defaultControlScheme = "KeyboardMouse";
        }
        else if (device == CrossPlatformInputManager.inputDevice.touch)
        {
            playerInputComponent.defaultControlScheme = "Touch";
        }
        else if (device == CrossPlatformInputManager.inputDevice.keyboardMouse)
        {
            playerInputComponent.defaultControlScheme = "KeyboardMouse";
        }
        else if (device == CrossPlatformInputManager.inputDevice.controller)
        {
            playerInputComponent.defaultControlScheme = "KeyboardMouse";
        }
    }
}
