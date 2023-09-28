using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTimer : MonoBehaviour
{
    [Header("Splash Screen Settings")]
    [Tooltip("What Level to Load After The Splash Screen")]
    [SerializeField] int levelToLoad;
    [Tooltip("How Much Time The Splash Screen Is Being Shown (Note: The Splash Screen Animation Is 03:30 Seconds")]
    [SerializeField] float screenTime;
    private void Update()
    {
        screenTime -= Time.deltaTime;
        if (screenTime <= 0 || Input.anyKey)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
