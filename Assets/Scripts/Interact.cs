using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.Player.Interact.performed += InteractAction;
    }
    
    private void OnDisable()
    {
        playerInput.Player.Interact.performed -= InteractAction;
        playerInput.Disable();
    }
    
    private GameObject currentInteractable;
    
    private void InteractAction(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action performed");
        if (currentInteractable == null) return;
        var interactableScript = currentInteractable.GetComponent<IInteractable>();
        interactableScript?.Interact(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable") && currentInteractable == other.gameObject)
        {
            currentInteractable = null;
        }
    }
}
