using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for UI buttons

public class PauseMenus : MonoBehaviour
{
    public GameObject pauseMenu;  // Pause Menu UI
    public Button resumeButton;   // Resume Button (Assign in Inspector)

    public static bool isPaused = false;
    public Player PlayerScript;

    void Start()
    {
        // Ensure pause menu starts hidden
        pauseMenu.SetActive(false);
        isPaused = false;
        PlayerScript.isPaused = false;

        // Manually assign Resume button function
        if (resumeButton != null)
        {
            resumeButton.onClick.RemoveAllListeners(); // Clear previous listeners
            resumeButton.onClick.AddListener(TogglePause); // Calls Pause/Resume like the 'P' key
        }
        else
        {
            Debug.LogError("Resume button is NOT assigned in the Inspector!");
        }
    }

    void Update()
    {
        // Press 'P' to toggle pause state
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