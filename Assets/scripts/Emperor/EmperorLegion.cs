using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorLegion : MonoBehaviour
{
    public Player owner;
    public float moveSpeed = -0.2f;
    private LegionState state = LegionState.Charging;

    public int damage = 1;

    public Quaternion originialRotation;
    public float Horizontal;
    public float Vertical;

    // Start is called before the first frame update
    void Start()
    {
        // Emperor/EmperorLegion
        // transform.Rotate(Vector3.up, 90f);

        // this.transform.rotation = Camera.main.transform.rotation;
        InvokeRepeating("MoveForward", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        if (owner.health > owner.MaxHealth/2)
        {
            state = LegionState.Protecting;
        }

        if (state == LegionState.Protecting)
        {
            Debug.Log("TODO: EmperorLegion protection stance");
        }

    }

    private void MoveForward()
    {
        if (state == LegionState.Charging)
        {
            Vector3 moveDirection = new Vector3(0, 0, 0.2f);
            this.transform.Translate(moveDirection);
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, owner.transform.position) > 50f)
        {
            state = LegionState.Idle;
        }

        this.transform.rotation = originialRotation;

        if (this.transform.position.y < 0)
        {
            var x = this.transform.position.x;
            var y = owner.transform.position.y;
            var z = this.transform.position.z;

            var newPos = new Vector3(x,y,z);
            this.transform.position = newPos;

        }

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 5f);
        Vector3 groundZero = this.transform.position;
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                UnityEngine.AI.NavMeshAgent pathFinder = hitCollider.GetComponent<UnityEngine.AI.NavMeshAgent>();
                Vector3 direction = enemy.transform.position - groundZero;
                float distance = direction.magnitude;

                if (distance <= 15f)
                {

                    if (distance > pathFinder.stoppingDistance)
                    {
                        Vector3 pushVector = direction.normalized * 10f;
                        pathFinder.velocity += pushVector;
                        enemy.health -= damage;
                    }
                }

            }
        }

    }

}

public enum LegionState
{
    Charging,
    Idle,
    Protecting
}