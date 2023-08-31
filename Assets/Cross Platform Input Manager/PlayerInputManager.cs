using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.PlayerInput;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 normalizedInput = context.action.ReadValue<Vector2>();

        playerController.inputMovementVector = normalizedInput;
    }

    public void Look(InputAction.CallbackContext context)
    {
        Vector2 normalizedInput = context.action.ReadValue<Vector2>();

        playerController.inputLookVector = normalizedInput;
    }

    public void Jump()
    {
    }

    public void Crouch(bool state)
    {
        if (state)
        {
            playerController.isCrouched = true;
        }
        else
        {
            playerController.isCrouched = false;
        }
    }

    public void SetRun(InputAction.CallbackContext context)
    {
        playerController.currentMovementState = PlayerController.movementState.run;
    }

    public void SetWalk(InputAction.CallbackContext context)
    {
        playerController.currentMovementState = PlayerController.movementState.walk;
    }
}
