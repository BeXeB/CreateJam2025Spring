using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    PlayerInput playerInput;
    Vector3 moveInput;
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
        playerInput.Player.Reload.performed += ReloadScene;
        playerInput.Player.LeaveChair.performed -= GetOutOfChair;
        playerInput.Disable();
    }
    
    private void Update()
    {
        var input = playerInput.Player.Move.ReadValue<Vector2>();
        moveInput = new Vector3(input.x, input.y, 0);
    }
    
    private void FixedUpdate()
    {
        var actualSpeed = inWheelchair ? speed : speed / 2;
        transform.position += moveInput * (actualSpeed * Time.fixedDeltaTime);
    }
    
    private void GetOutOfChair(InputAction.CallbackContext obj)
    {
        if (!inWheelchair)
        {
            return;
        }
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
