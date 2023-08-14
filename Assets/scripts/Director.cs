using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Netcode;

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

/// <summary>
/// This contains both positive and negative effects ( negative numbers for negative effects (for the player), positive numbers for postive effects )
/// </summary>
public enum Effects
{
    NanoBotInfusion = 1, // Buff friends units (players only) - 30 seconds
    Subjugate = 2, // Single enemy 50% armor reduction
    Conquer = 3, // 5% nearby enemy armor reduction
    Tryant = 4, // Given to EmperorClass when Subjugate/Conquer are active, granting 30% armor increase
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

    public List<GameObject> players = new List<GameObject>();

    public bool enable = true;

    private int NextId = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("[Director]: Ready! Let the show begin!");

        getAllPlayers();
    }

    void getAllPlayers()
    {
        UnityEngine.Object[] tempList = GameObject.FindObjectsOfType(typeof(MonoBehaviour));

        foreach (UnityEngine.Object obj in tempList)
        {
            
            // Debug.Log("loop");
            if (obj is Player)
            {
                Player player = (Player)obj;
                GameObject realObject = player.gameObject;

                Player p = realObject.GetComponent<Player>();
                if (realObject.hideFlags == HideFlags.None && p != null && !players.Contains(realObject) )
                {
                    // Ignore chairman class
                    if (p.pClass != PlayerClass.Chairman)
                    {
                        players.Add(realObject);
                    }
                }

            }
        }
    }

    private Transform FindBestTarget(Transform MyPosition)
    {
        float lowest_distance = float.MaxValue;
        Transform target = null;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, MyPosition.position);
            if (distance < lowest_distance)
            {
                target = player.transform;
            }
        }

        return target;
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) { return; }

        foreach (GameObject enemy in enemies.Values )
        {
            var pathfinder = enemy.GetComponent<Pathfinder>();

            Transform target = FindBestTarget(enemy.transform);

            if (target != null)
            {
                pathfinder.target = target;
            }
        }

        getAllPlayers(); // Change this to once per stage

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

                // enemyEntity.GetComponent<NetworkObject>().Spawn();

                var logic = enemyEntity.GetComponent<EnemyLogic>();

                logic.type = (EnemyTypes)enemy;
                logic.active = true;
                logic.body = enemyEntity;
                logic.director = this;
                logic.EnemyID = NextId;

                var pathfinder = enemyEntity.GetComponent<Pathfinder>();

                Transform target = FindBestTarget(enemyEntity.transform);

                if (target != null)
                {
                    pathfinder.target = target;
                }

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
