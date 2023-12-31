using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorSoldier : MonoBehaviour
{
    public int damage = 20;
    public int MaxHealth = 5;
    public int Health;

    public int TimeAlive = 0;

    public Player owner;
    private Pathfinder pathFiner;
    private Director director;

    // Start is called before the first frame update
    void Start()
    {
        pathFiner = this.GetComponent<Pathfinder>();
        director = GameObject.Find("Director").GetComponent<Director>();
        Health = MaxHealth;

        InvokeRepeating("Decay", 2.0f, 1f);
    }

    private void Decay()
    {
        TimeAlive += 1;
        if (TimeAlive > 30)
        {
            Health -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Health <=0)
        {
            Destroy(this.gameObject);
            return;
        }

        var ownerPosition = owner.transform.position;
        int closest = int.MaxValue;
        Transform goal = null;
        foreach (var enemy in director.enemies.Values)
        {
            float distance = Vector3.Distance(ownerPosition, enemy.transform.position);
            if (distance < closest)
            {
                goal = enemy.transform;
            }

        }

        if (goal != null)
        {
            pathFiner.target = goal;
        }

        Vector3 position = this.gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            var enemy = hitCollider.GetComponent<EnemyLogic>();
            if (enemy != null && enemy.gameObject != null)
            {
                enemy.health -= damage;
            }
        }

    }
}
