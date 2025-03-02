using UnityEngine;
using UnityEngine.AI;

public class InvisTesting : MonoBehaviour
{
    private Camera mainCam;
    private Plane[] frustrumplanes;
    private Collider collider;
    [SerializeField]
    private NavMeshAgent agent;

    private GameObject player;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = gameObject.GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
