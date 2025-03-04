using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehavior : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Resart(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level 1");
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}
