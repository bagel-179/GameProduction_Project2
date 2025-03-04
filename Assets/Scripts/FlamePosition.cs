using UnityEngine;

public class FlamePosition : MonoBehaviour
{
    public GameObject FlameCenter;
    public GameObject LanternFlame;

    void Update()
    {
        LanternFlame.transform.rotation = FlameCenter.transform.rotation;
    }
}
