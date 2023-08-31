using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.DefaultInputActions;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputMovementVector, inputLookVector;

    private CharacterController characterController;

    public enum movementState
    {
        walk,
        run
    }

    public movementState currentMovementState = movementState.walk;

    public GameObject head, body;

    [SerializeField] private float mouseSensitivity = 10.0f;
    public float walkSpeed = 10.0f;
    public float runSpeed = 20.0f;
    public bool useGlobalGravity;

    public float gravity = Physics.gravity.y;

    public float moveSpeed = 10.0f;

    public float gravityMultiplier = 1.0f;
    public float actualGravity;
    public bool isGrounded, isCrouched = false;

    public float headRotation = 0.0f;
    public Vector3 movementVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (useGlobalGravity)
        {
            actualGravity = Physics.gravity.y * gravityMultiplier;
        }
        else
        {
            actualGravity = gravity * gravityMultiplier;
        }

        if (currentMovementState == movementState.walk)
        {
            moveSpeed = walkSpeed;
        }
        else if (currentMovementState == movementState.run)
        {
            moveSpeed = runSpeed;
        }

        if (isCrouched)
        {
            head.transform.localPosition = Vector3.Slerp(head.transform.localPosition, new Vector3(0, 0.5f, 0), 5f * Time.deltaTime);
        }
        else
        {
            head.transform.localPosition = Vector3.Slerp(head.transform.localPosition, new Vector3(0, 1.5f, 0), 5f * Time.deltaTime);
        }

        try
        {
            MouseLookUpdate();
        }
        catch (ArgumentException ex)
        {
            Debug.LogError("Mouse look error caught: " + ex);
        }

        try
        {
            MovementUpdate();
        }
        catch (ArgumentException ex)
        {
            Debug.LogError("Movement error caught: " + ex);
        }

        try
        {
            PhysicsUpdate();
        }
        catch (ArgumentException ex)
        {
            Debug.LogError("Physics error caught: " + ex);
        }
    }

    private void MouseLookUpdate()
    {
        float mouseX = inputLookVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = inputLookVector.y * mouseSensitivity * Time.deltaTime;

        headRotation -= mouseY;
        headRotation = Mathf.Clamp(headRotation, -75, 75);

        transform.Rotate(Vector3.up * mouseX);
        head.transform.localRotation = Quaternion.Euler(headRotation, 0f, 0f);
    }

    private void MovementUpdate()
    {
        float moveX = inputMovementVector.x;
        float moveZ = inputMovementVector.y;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    private void PhysicsUpdate()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded && movementVelocity.y < 0)
        {
            movementVelocity.y = -2f;
        }

        movementVelocity.y += actualGravity * Time.deltaTime;
        characterController.Move(movementVelocity * Time.deltaTime);
    }
}
