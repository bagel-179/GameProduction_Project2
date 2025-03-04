using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenus : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;

    public static bool isPaused = false;
    public Player PlayerScript;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        PlayerScript.isPaused = false;

        /*if (resumeButton != null)
        {
            resumeButton.onClick.RemoveAllListeners();
            resumeButton.onClick.AddListener(TogglePause);
        }
        else
        {
            Debug.LogError("Resume button is NOT assigned in the Inspector!");
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        PlayerScript.isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        PlayerScript.isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}