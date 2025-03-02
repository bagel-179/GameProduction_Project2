using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject player;   
    public bool isStopped;
    private NavMeshAgent agent;
    private MeshRenderer mesh;
    private bool onCooldown;
    private float cooldownTimer = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (!isStopped)
        {
            if (distance > 0)
            {
                agent.destination = player.transform.position;
            }
        }

        if (isStopped)
        {
            if (!onCooldown)
            {
                agent.isStopped = true;
                StartCoroutine(Cooldown());
                StartCoroutine(frozen());
                agent.isStopped = false;
                agent.destination = player.transform.position;

            }
            //start a coroutine for how long you will be frozen for
            // iterate on idea to make that time more interactive for the player
            //there also needs to be a lock out/cooldown on this coroutine
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
        
        yield return new WaitForSeconds(stunTimer);
        agent.isStopped = false;
    }
}
