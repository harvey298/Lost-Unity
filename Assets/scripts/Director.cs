using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum EnemyTypes
{
    Goblin = 10,
    Elf = 20,
    Orc = 30,
    Troll = 40,
    Zombie = 50,
    Skeleton = 60,
    Dragon = 70,
    Phantom = 80,
    PhantomGoblin = 90,
    PhantomElf = 100,
    PhantomOrc = 110,
    PhantomTroll = 120,
    PhantomZombie = 130,
    PhantomSkeleton = 140,
    PhantomDragon = 150,
    PhantomPhantom = 160,
}

public class Director : MonoBehaviour
{

    public int credits = 0;
    public int creditGainRate = 1;

    public int entitiyLimit = 10;

    public int maxCredits = 160;

    public int difficulity = 1;

    public bool GamePaused = false;

    public GameObject EnemyPrefab;

    public Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();

    public GameObject player;

    public bool enable = true;

    private int NextId = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Director-Debug]: Reminder that sharing direct memory access is a status access violation! Pointers only!");
        Debug.Log("[Director]: Ready! Let the show begin!");
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) { return; }

        // Credit Calculation
        if (credits < maxCredits)
        {
            credits += creditGainRate * difficulity;
        }

        if (!(enemies.Count >= entitiyLimit))
        {
            var enemy = BuyEnemy();

            if (enemy != null)
            {

                GameObject enemyEntity = Instantiate(EnemyPrefab); // , transform.position, transform.rotation

                var logic = enemyEntity.GetComponent<EnemyLogic>();

                logic.type = (EnemyTypes)enemy;
                logic.active = true;
                logic.body = enemyEntity;
                logic.director = this;
                logic.EnemyID = NextId;

                var pathfinder = enemyEntity.GetComponent<Pathfinder>();
                pathfinder.target = player.transform;

                enemies.Add(NextId, enemyEntity);
                
                NextId += 1;

            }
        }
    }

    public void despawn_entity(int entity_id)
    {
        enemies.Remove(entity_id);
    }

    private EnemyTypes? BuyEnemy()
    {
        var rng = UnityEngine.Random.Range(10f, 160f);
        int priceGoal = (int)((int)Mathf.Round(rng / 10f) * 10f);

        EnemyTypes goal = (EnemyTypes)Enum.ToObject(typeof(EnemyTypes), priceGoal);

        int reminaing_credits = credits - (int)goal;

        if (reminaing_credits < 0) { return null; }

        return goal;
    }
}
