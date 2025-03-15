using System;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;

    public void GameOver()
    {
        menu.SetActive(true);
    }

    public void Restart()
    {
        GameManager.instance.Reload();
        menu.SetActive(false);
    }

    public void Menu()
    {
        GameManager.instance.Menu();
        menu.SetActive(false);
    }

    private void OnEnable()
    {
        InputManager.instance.playerInput.Disable();
    }

    private void OnDisable()
    {
        InputManager.instance.playerInput.Enable();
    }
}
