using UnityEngine;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded;
    public void WinLevel()
    {
        if (!gameEnded)
        {
            Debug.Log("You Win!");
            gameEnded = true;
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
