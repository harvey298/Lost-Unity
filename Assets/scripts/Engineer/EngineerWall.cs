using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject obj;
    private int starting_decay_time = 1400;
    public int decay_time = 1000;
    public bool decaying = false;
    public bool generator = false;

    public GameObject owner;

    private List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("check_damage", 2.0f, 0.3f);
        InvokeRepeating("decay_owner", 2.0f, 0.4f);
        decay_time = starting_decay_time;
    }

    // CHecks to see if an enemy is due to be damaged
    private void check_damage()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<EnemyLogic>().health -= 1;
                var owner_data = owner.GetComponent<EngineerClass>();
                if (owner_data.enegery != 100)
                {
                    owner_data.enegery += 1;
                }

            }

        }

    }

    // Lowers the owners engery level
    private void decay_owner()
    {
        if (generator) 
            owner.GetComponent<EngineerClass>().enegery -= 2;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!decaying) { return; }

        // Supress Decay is suffiect enegery is available
        if (owner.GetComponent<EngineerClass>().enegery < 90)
        {
            decay_time -= 1;
        }        

        if (decay_time <= 0)
        {
            enemies.Clear();

            Destroy(this.gameObject);
            return;
        }

        if (generator)
        {
            Vector3 position = this.gameObject.transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(position, 10f);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyLogic>();
                if (enemy != null && enemy.gameObject != null)
                {
                    if (!enemies.Contains(enemy.gameObject))
                    {
                        enemies.Add(enemy.gameObject);
                    }
                    
                }
            }
        }

    }
}
