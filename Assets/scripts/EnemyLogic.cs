using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    public EnemyTypes type;
    public int EnemyID;

    public bool active = false;

    public int health = 20;

    public Director director;

    public GameObject body;

    // Effect, Time remaining
    public Dictionary<Effects, int> effects = new Dictionary<Effects, int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void add_effect(Effects effect, int length)
    {
        effects.Add(effect, length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) { return; }

        if (health <= 0)
        {
            Debug.Log("Despawning");
            director.despawn_entity(EnemyID);
            Destroy(body);
            return;            
        }
        
    }
}
