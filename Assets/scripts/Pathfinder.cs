using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    public Transform target; // The target location to move towards
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public bool stunned = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component on this GameObject
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Stun Checker
        if (stunned)
        {
            agent.isStopped = true;
        } else if (!stunned && agent.isStopped)
        {
            agent.isStopped = false;
        }

        if (target != null)
        {
            // Set the destination for the NavMeshAgent
            agent.SetDestination(target.position);
        }
    }
}
