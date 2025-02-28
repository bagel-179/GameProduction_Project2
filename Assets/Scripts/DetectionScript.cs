using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectionScript : MonoBehaviour
{
    /// <summary>
    /// Needs to be worked on more so im running with a different idea
    /// </summary>
    public bool isSpotted;
    private MeshRenderer renderer;
    public GameObject Enemy;
    private Plane[] cameraFrustum;
    private Camera firstperson;
    private Collider collider;

    private void Start()
    {
        firstperson = Camera.main;
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(firstperson);

        if (GeometryUtility.TestPlanesAABB(cameraFrustum, collider.bounds))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Spotted");
            }
        }

    }
}
