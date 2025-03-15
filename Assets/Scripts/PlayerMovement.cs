using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    
    PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    
    private void OnEnable()
    {
        playerInput.Enable();
    }
    
    private void OnDisable()
    {
        playerInput.Disable();
    }
    
    private void Update()
    {
        var input = playerInput.Player.Move.ReadValue<Vector2>();
        var move = new Vector3(input.x, input.y, 0);
        transform.position += move * (speed * Time.deltaTime);
    }
}
