using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public WinLoose winLooseScript;

    private void OnTriggerEnter(Collider other)
    {
        winLooseScript.WinLevel();
    }
}
