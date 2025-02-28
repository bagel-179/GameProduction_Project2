using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    public bool isStopped;
    [SerializeField]
    private bool onCooldown;
    [SerializeField]
    private float cooldownTimer = 5f;
    [SerializeField]

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > 0)
        {
            Pathfinding();
        }

        if (isStopped)
        {
            if (!onCooldown)
            {
                agent.isStopped = true;
                StartCoroutine(Cooldown());
                StartCoroutine(frozen());
                agent.isStopped = false;

            }
            //start a coroutine for how long you will be frozen for
            // iterate on idea to make that time more interactive for the player
            //there also needs to be a lock out/cooldown on this coroutine
        }
        
        
    }

    public IEnumerator Pathfinding()
    {
        yield return new WaitForSeconds(0.2f);
        if (!isStopped)
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
        float stunTimer = Random.Range(1, 3);
        //changes something about the manniquin
        // change color
        Invis();
        
        yield return new WaitForSeconds(stunTimer);
        agent.isStopped = false;
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

    void Invis()
    {
        foreach (var mesh in meshes)
        {
            mesh.enabled = false;
        }
        
    }
}
