using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject volumeObject;

    private void Awake()
    {
        InputManager.instance.playerInput.Disable();
        GameManager.instance.player.SetActive(false);
    }

    public void Play()
    {
        GameManager.instance.player.SetActive(true);
        InputManager.instance.playerInput.Enable();
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void VolumeMenu()
    {
        menuObject.SetActive(false);
        volumeObject.SetActive(true);
    }

    public void ChangeVolume()
    {
        
    }

    public void BackToMenu()
    {
        volumeObject.SetActive(false);
        menuObject.SetActive(true);
    }
}
