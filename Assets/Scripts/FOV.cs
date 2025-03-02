using System;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class FOV : MonoBehaviour
{

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask enemyMask;
    public LayerMask wallMask;
    public bool isSpotted;
    

    void FindEnemy()
    {
        isSpotted = false;
        Collider[] targets = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);

        for (int i = 0; i < targets.Length; i++)
        {
            Transform enemy = targets[i].transform;
            Vector3 enemyDirection = (enemy.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, enemyDirection) < viewAngle / 2)
            {
                float distToEnemy = Vector3.Distance(transform.position, enemy.position);
                if (!Physics.Raycast(transform.position, enemyDirection, distToEnemy, wallMask))
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        isSpotted = true;
                        var script = enemy.GetComponent<Enemy>();

                        script.isStopped = true;

                    }
                    
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool isGlobalAngle)
    {
        if (!isGlobalAngle)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemy();
    }
}
