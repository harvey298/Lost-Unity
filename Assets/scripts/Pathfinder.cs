using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{
    public Transform target; // The target location to move towards
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public bool stunned = false;
    
    private float stunTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component on this GameObject
        InvokeRepeating("decrease_stun_time", 2.0f, 0.3f);
    }

    private void decrease_stun_time()
    {
        if (stunTime <= 0)
        {

            stunned = false;

        } else
        {
            stunTime -= 1;
        }

        
    }

    public void stun(float stun_time)
    {
        stunned = true;
        stunTime = stun_time;
    }

    public void stun(float stun_time, bool disableAgent)
    {
        if (disableAgent)
        {
            disable_agent();
        }
        stunned = true;
        stunTime = stun_time;
    }

    private void disable_agent()
    {

        Debug.Log("Disabling Agent");
        agent.enabled = false;
        agent.updateRotation = false;
        agent.updatePosition = false;
        
    }

    private void enable_agent()
    {
        agent.updateRotation = true;
        agent.updatePosition = true;
        agent.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.gameObject.transform.position.y < 0)
        {
            float x = this.gameObject.transform.position.x;
            float z = this.gameObject.transform.position.z;

            Vector3 newPos = new Vector3(x, 1f, z);
            this.gameObject.transform.position = newPos;
        }

        if (!agent.enabled)
        {
            if (!stunned)
            {
                enable_agent();
            }

            return;
        }

        if (stunned)
        {
            disable_agent();
            return;
        }

        // Stun Checker
        //if (stunned)
        //{
        //    agent.isStopped = true;

//            return;
  //      } else if (!stunned && agent.isStopped)
    //    {
      //      agent.isStopped = false;
        //}

        if (target != null && agent.enabled)
        {
            // Set the destination for the NavMeshAgent
            agent.SetDestination(target.position);
        }
    }
}
