using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
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
    private MeshRenderer[] meshes;
    private NavMeshPath path;
    
    /// <summary>
    /// The in code values and variables
    /// </summary>
    [FormerlySerializedAs("isStopped")] public bool isSeen;
    [SerializeField]
    private bool onCooldown;
    [SerializeField]
    private float cooldownTimer = 6f;
    [SerializeField]
    public float safezone = 3f;
    public Animator anim;
    [SerializeField]
    private bool isFrozen;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        

        if (isSeen)
        {
            if (!onCooldown)
            {
                if (distance <= safezone)
                {

                    Debug.Log("I am Stopped");
                    StartCoroutine(Cooldown());
                    StartCoroutine(frozen());
                    //StartCoroutine(Pathfinding());

                }
                else if(!isFrozen)
                {
                    StartCoroutine(Pathfinding());
                }
            }
           
        }
        else if (distance > 0 && !isFrozen)
        {
            StartCoroutine(Pathfinding());
        }

        isSeen = false;
    }

    public IEnumerator Pathfinding()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        if (!isSeen)
        {
            agent.CalculatePath(player.transform.position, path);
            agent.SetPath(path);
        }
    }
    

    public IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
    }

    public IEnumerator frozen()
    {
        agent.isStopped = true;
        anim.enabled = false;
        isFrozen = true;
        float stunTimer = Random.Range(1, 3);
        yield return new WaitForSeconds(stunTimer);
        isFrozen = false;
        anim.enabled = true;
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
    
}
