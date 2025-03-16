using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput playerInput;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        
        instance = this;
        playerInput = new PlayerInput();
        playerInput.Dialogue.Disable();
    }
}
