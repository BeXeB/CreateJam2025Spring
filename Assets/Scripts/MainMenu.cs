using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private GameObject volumeObject;

    public void Play()
    {
        SceneManager.LoadScene(1);
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
