using System.Collections;
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
    [FormerlySerializedAs("onCooldown")] [SerializeField]
    private bool canHide;
    private NavMeshPath path;
    private Vector3 target;
    private bool isInCamera;
    private Plane[] planes;
    private Collider collider;
    private bool isFrozen;
    private bool freezeCooldown;
    
    

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
            
            switch (canHide)
            {
                // you caught the enemy but he can't hide right now
                case true:
                    StartCoroutine(frozen());
                    break;
                // caught the enemy and it can hide
                case false:
                    if (!isFrozen && !freezeCooldown)
                    {
                        StartCoroutine(frozen());
                        StartCoroutine(FreezeCooldown(6f));
                        StartCoroutine(Pathfinding(player.transform.position));
                    }
                    break;
                    
            }
        }
        if (distance > 0 && !isFrozen)
        {
            StartCoroutine(Pathfinding(player.transform.position));
        }
        

        
        
        
    }

    public IEnumerator CameraCheck()
    {
        if (canHide)
        {
            int temp = 0;
            while (temp == 0)
            {
                if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
                {
                    Debug.Log("Your still staring at me!! I cant hide");
                }

                if (!GeometryUtility.TestPlanesAABB(planes, collider.bounds))
                {
                    temp = 1;
                    Hiding();
                }
            }
            yield return null;
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
        if(canHide)
        {
            isSeen = false;
            agent.isStopped = false;
            Invis(true);
            transform.position = hidingSpots[Random.Range(0, hidingSpots.Length)].position;
            StartCoroutine(Pathfinding(player.transform.position));
            if (Vector3.Distance(transform.position, target) < 2f)
            {
                Invis(false);
                StartCoroutine(FreezeCooldown(9f));
            }
        }
        
    }
    

    public IEnumerator FreezeCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        isFrozen = false;
    }

    public IEnumerator frozen()
    {
        isFrozen = true;
        agent.isStopped = true;
        float stunTimer = Random.Range(2, 4);
        if (canHide)
        {
            StartCoroutine(CameraCheck());
        }
        yield return new WaitForSeconds(stunTimer);
        agent.isStopped = false;
        isSeen = false;
        isFrozen = false;
        freezeCooldown = true;
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
