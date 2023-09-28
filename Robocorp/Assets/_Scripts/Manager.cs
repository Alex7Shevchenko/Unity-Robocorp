using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Manager : MonoBehaviour
{
    [SerializeField] bool pauseMenuOption;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject player;
    [SerializeField] CinemachineVirtualCamera[] mainCamera;
    [SerializeField] CinemachineFreeLook mainCamera2;

    public bool pauseMenuActive;

    private void Update()
    {
        PauseMenuActivation();
    }

    public void LoadMainScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;
    }

    public void CloseProgram()
    {
        Application.Quit();
    }

    public void Disable(GameObject gameobject)
    {
        gameobject.SetActive(false);
    }

    public void Enable(GameObject gameobject)
    {
        gameobject.SetActive(true);
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseMenuActivation()
    {
        if (pauseMenuOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseMenuActive = !pauseMenuActive;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuActive)
            {
                Time.timeScale = 0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                for(int i = 0; i < mainCamera.Length; i++)
                {
                    mainCamera[i].enabled = false;
                }
                mainCamera2.enabled = false;
                pauseMenu.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuActive)
            {
                ClosePauseMenu();
            }
        }
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < mainCamera.Length; i++)
        {
            mainCamera[i].enabled = true;
        }
        mainCamera2.enabled = true;
        pauseMenu.SetActive(false);
        pauseMenuActive = false;
    }
}
