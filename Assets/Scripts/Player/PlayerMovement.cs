using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 3f;
    [SerializeField] private float deceleration = 2f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float outOfChairSpeedMultiplier = 0.2f;
    [SerializeField] private float outOfChairRotationMultiplier = 0.5f;
    
    private PlayerInput playerInput;
    private float currentSpeed = 0f;
    private bool _shouldGoForward;
    private bool _shouldGoBackward;
    private bool _shouldTurnLeft;
    private bool _shouldTurnRight;
    
    public bool inWheelchair;
    public Wheelchair wheelchair;

    private void Awake()
    {
        playerInput = InputManager.instance.playerInput;
    }
    
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.LeaveChair.performed += GetOutOfChair;
        playerInput.Player.Reload.performed += ReloadScene;
    }
    
    private void OnDisable()
    {
        playerInput.Player.LeaveChair.performed -= GetOutOfChair;
        playerInput.Player.Reload.performed -= ReloadScene;
        playerInput.Disable();
    }
    
    private void Update()
    {
        _shouldGoForward = playerInput.Player.MoveForward.ReadValue<float>() == 1;
        _shouldGoBackward = playerInput.Player.MoveBackward.ReadValue<float>() == 1;
        _shouldTurnLeft = playerInput.Player.RotateLeft.ReadValue<float>() == 1;
        _shouldTurnRight = playerInput.Player.RotateRight.ReadValue<float>() == 1;
    }
    
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }
    
    private void HandleMovement()
    {
        if (inWheelchair)
        {
            if (_shouldGoForward)
            {
                currentSpeed += acceleration * Time.fixedDeltaTime;
            }
            else if (_shouldGoBackward)
            {
                currentSpeed -= acceleration * Time.fixedDeltaTime;
            }
            else
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0);
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed += deceleration * Time.fixedDeltaTime;
                    currentSpeed = Mathf.Min(currentSpeed, 0);
                }
            }
            
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
        }
        else
        {
            currentSpeed = (_shouldGoForward ? maxSpeed * outOfChairSpeedMultiplier : _shouldGoBackward ? -maxSpeed * outOfChairSpeedMultiplier : 0);
        }
        
        transform.position += transform.up * (currentSpeed * Time.fixedDeltaTime);
    }
    
    private void HandleRotation()
    {
        float currentRotationSpeed = inWheelchair ? rotationSpeed : rotationSpeed * outOfChairRotationMultiplier;
        
        if (_shouldTurnLeft && !_shouldTurnRight)
        {
            transform.Rotate(Vector3.forward * (currentRotationSpeed * Time.fixedDeltaTime));
        }
        if (_shouldTurnRight && !_shouldTurnLeft)
        {
            transform.Rotate(Vector3.forward * (-currentRotationSpeed * Time.fixedDeltaTime));
        }
    }

    private void GetOutOfChair(InputAction.CallbackContext obj)
    {
        if (!inWheelchair) return;
        
        inWheelchair = false;
        wheelchair.transform.SetParent(null);
        wheelchair = null;
    }
    
    private void ReloadScene(InputAction.CallbackContext obj)
    {
        GameManager.instance.Reload();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("End"))
        {
            GameManager.instance.LoadNext();
        }
    }
}
