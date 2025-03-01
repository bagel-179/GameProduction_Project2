using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    
    /// <summary>
    /// The in code values and variables
    /// </summary>
    [FormerlySerializedAs("isStopped")] public bool isSeen;
    [SerializeField]
    private bool onCooldown;
    [SerializeField]
    private float cooldownTimer = 20f;
    private NavMeshPath path;
    private Vector3 target;
    private bool isFrozen;
    private Plane[] planes;
    private Collider collider;
    
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        Vector3 target = player.transform.position;
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        collider = GetComponent<Collider>();


    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        

        if (isSeen)
        {
            if (!onCooldown)
            {
                agent.isStopped = true;
                isFrozen = true;
                StartCoroutine(CameraCheck());
                StartCoroutine(Cooldown());

            }
            //start a coroutine for how long you will be frozen for
            // iterate on idea to make that time more interactive for the player
            //there also needs to be a lock out/cooldown on this coroutine
        }
        else if (distance > 0)
        {
            StartCoroutine(Pathfinding(target));
        }
        
        
    }

    public IEnumerator Pathfinding(Vector3 destination)
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        if (!isSeen)
        {
            agent.CalculatePath(destination, path);
            agent.SetPath(path);
        }
    }

    void Hiding()
    {
        if(!onCooldown)
        {
            Invis(true);
            float maxTimeHidden = 5f;
            transform.position = hidingSpots[Random.Range(0, hidingSpots.Length)].position;
            StartCoroutine(Pathfinding(target));
            if (Vector3.Distance(transform.position, target) < 2f)
            {
                Invis(false);
            }
        }
        
    }

    IEnumerator CameraCheck()
    {
        bool inVision;
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
        {
            inVision = true;
        }
        else
        {
            inVision = false;
            Hiding();
        }

        yield return null;
    }
    

    public IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
    }

    public IEnumerator frozen()
    {
        float stunTimer = Random.Range(1, 3);
        //changes something about the manniquin
        // change color
        
        yield return new WaitForSeconds(stunTimer);
        agent.isStopped = false;
        isSeen = false;
    }

    void Flee(float fleeDistance)
    {
        if (fleeDistance > 0)
        {
            Vector3 temp = player.transform.position - transform.position;
            Vector3 fleeDirection = transform.position - temp;

            agent.destination = fleeDirection;
        }
        
    }

    void Invis(bool isInvis)
    {
        if (isInvis)
        {
            foreach (var mesh in meshes)
            {
                mesh.enabled = true;
            }
        }

        if (!isInvis)
        {
            foreach (var mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
        
    }
}
