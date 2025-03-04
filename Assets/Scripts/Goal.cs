using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public WinLoose winLooseScript;
    private bool Win;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winLooseScript.WinLevel();
            Win = true;
            Debug.Log("YES");
        }

    }

}

