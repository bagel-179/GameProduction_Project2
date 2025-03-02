using UnityEngine;

public class FlamePosition : MonoBehaviour
{
    public GameObject FlameCenter;
    public GameObject LanternFlame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LanternFlame.transform.rotation = FlameCenter.transform.rotation;
    }
}
