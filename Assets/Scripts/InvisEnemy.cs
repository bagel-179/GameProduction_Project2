using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
public class InvisEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// These are the values that have to be in the inspector
    /// 1. The player thats currently inside of the scene
    /// 2. The Navmesh Agent that should be attached to the enemy
    /// 3. The Meshrenderer that is attached to the eyes, head, and body
    /// </summary>
    [SerializeField]
    public GameObject player;   
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private SkinnedMeshRenderer[] meshes;
    [SerializeField] 
    private Transform[] hidingSpots;
    public float safezone = 3f;
    
    /// <summary>
    /// The in code values and variables
    /// </summary>
    [FormerlySerializedAs("isStopped")] public bool isSeen;
    [FormerlySerializedAs("onCooldown")] [SerializeField]
    private bool canHide;
    private NavMeshPath path;
    private Vector3 target;
    private bool isHiding;
    private Plane[] planes;
    private Collider collider;
    private bool isLooking;
    [SerializeField] private bool onCooldown;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        collider = GetComponent<Collider>();
        canHide = true;
        


    }
    void Update()
    {
        
        float distance = Vector3.Distance(player.transform.position, transform.position);
        
        
        if (isSeen)
        {
            if (!isHiding)
            {
                agent.isStopped = true;
                isLooking = true;
                StartCoroutine(LookingAway());
            }
            
        }
        else if (!isSeen && distance > 0)
        {
            agent.isStopped = false;
            StartCoroutine(Pathfinding(player.transform.position));
        }

        if (isHiding)
        {
            if (distance <= safezone)
            {
                Invis(false);
                StartCoroutine(HideCooldown());
                isHiding = false;
            }
        }
        

        
        
        isSeen = false;
    }

    public IEnumerator LookingAway()
    {
        if (isLooking)
        {
            var timer = 10f;
            timer -= Time.timeScale;
            while (timer > 0)
            {
                if (!isSeen && canHide)
                {
                    Hiding();
                   
                }

                yield return null;
            }
            
        }
        
        isLooking = false;
        
    }
    public IEnumerator HideCooldown()
    {
        canHide = false;
        yield return new WaitForSeconds(9f);
        canHide = true;
    }
    

    public IEnumerator Pathfinding(Vector3 destination)
    {
        yield return new WaitForSeconds(0.2f);
        
        if (!isSeen)
        {
            agent.CalculatePath(destination, path);
            agent.SetPath(path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    void Hiding()
    {
        if(canHide && !isHiding)
        {
            Debug.Log("I'm being triggered");
            canHide = false;
            isSeen = false;
            agent.isStopped = false;
            isHiding = true;

            Debug.Log("Cloaking");
            Invis(true);
            transform.position = hidingSpots[Random.Range(0, hidingSpots.Length)].position;
            StartCoroutine(Pathfinding(player.transform.position));
            canHide = false;


        }
        
    }
    

    void Invis(bool isInvis)
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = !isInvis;
        }
        
    }
    
    
}
