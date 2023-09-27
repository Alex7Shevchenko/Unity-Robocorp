using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public void Printer()
    {
        print("Pressed");
    }

    public void LoadMainScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
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
}
