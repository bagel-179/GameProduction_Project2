using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded = false;
    public void WinLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Win!");
            gameEnded = true;
            SceneManager.LoadScene("WinScene");
        }
    }

    public void LooseLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Loose!");
            gameEnded = true;
        }

    }

}
