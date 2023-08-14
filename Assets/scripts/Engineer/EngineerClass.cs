using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerClass : MonoBehaviour
{
    private int ultimateCooldownRemaining = 0;

    private int altFireCooldownRemaining = 0;

    public int enegery = 100;
    public int damage = 20;

    const string WALL_PREFAB_PATH = "Engineer/EngineerWall";

    private GameObject wallPrefab;

    private GameObject LookingAt;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
        wallPrefab = Resources.Load<GameObject>(WALL_PREFAB_PATH);
        InvokeRepeating("IncreaseEnergy", 2.0f, 0.3f);
        InvokeRepeating("decrease_cool_downs", 2.0f, 1f);

        cam = Camera.main;

    }

    private void decrease_cool_downs()
    {
        if (ultimateCooldownRemaining < 0) { ultimateCooldownRemaining = 0;  }
        if (ultimateCooldownRemaining > 0) { ultimateCooldownRemaining -= 1; }

        if (altFireCooldownRemaining < 0) { altFireCooldownRemaining = 0; }
        if (altFireCooldownRemaining > 0) { altFireCooldownRemaining -= 1; }
    }

    void Update()
    {
        if (wallPrefab == null)
        {
            wallPrefab = Resources.Load<GameObject>(WALL_PREFAB_PATH);
        }

        // Fortifcation Protcol
        if (Input.GetKeyDown(KeyCode.R) && ultimateCooldownRemaining == 0 && enegery >= 90)
        {
            ultimateCooldownRemaining = 10;

            PointGenerator point_gen = new PointGenerator();

            point_gen.player = this.gameObject;

            GameObject gen = Instantiate(Resources.Load<GameObject>("Engineer/EngineerGenerator"));

            Vector3 gen_pos = this.gameObject.transform.position;
            gen_pos.y += 2f;
            gen.transform.position = gen_pos;

            Wall gen_data = gen.GetComponent<Wall>();
            gen_data.owner = this.gameObject;
            gen_data.generator = true;
            gen_data.decaying = true;

            var points = point_gen.GeneratePointsAroundPlayer(10f);

            for (int i = 0; i < points.Count; i++)
            {
                Vector3 point = points[i];
                GameObject wall = Instantiate(wallPrefab);
                wall.transform.position = point;
                Wall data = wall.GetComponent<Wall>();
                data.decaying = true;
                data.owner = this.gameObject;
            }

            enegery -= 90;
            
        }

        // Sentinel Pulse
        if (Input.GetKeyDown(KeyCode.LeftShift) && enegery >= 50)
        {
            Collider[] hitColliders = Physics.OverlapSphere(cam.transform.position, 50f);
            Vector3 groundZero = cam.transform.position;
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
                        }
                    }

                }
            }

            enegery -= 50;
        }

        // Nanobot Rifle
        if (Input.GetMouseButtonDown(0) && LookingAt != null)
        {
            // Destroy the hit object
            var enemy = LookingAt.GetComponent<EnemyLogic>();
            if (enemy != null)
            {
                enemy.health -= damage;
                LookingAt = null;
            }

        }

        // Nanobot Infusion
        if (Input.GetMouseButtonDown(1) && altFireCooldownRemaining == 0)
        {
            if (LookingAt != null)
            {
                var player = LookingAt.GetComponent<remote_player>();
                if (player != null)
                {
                    player.gameObject.GetComponent<Player>().effects.Add(Effects.NanoBotInfusion, 30);
                    altFireCooldownRemaining = 3;
                    LookingAt = null;
                }

            } else
            {
                altFireCooldownRemaining = 3;
                this.gameObject.GetComponent<Player>().effects.Add(Effects.NanoBotInfusion, 30);
            }

        }

    }

    private void FixedUpdate()
    {
        // Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        // Ray ray = cam.ScreenPointToRay(screenCenter);

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward , out hit))
        {

            // Debug.DrawRay(ray.origin, ray.direction * 100f, Color.black, 1f);

            if (hit.collider != null && LookingAt != hit.collider.gameObject && hit.collider.gameObject.GetComponent<EnemyLogic>() != null)
            {
                // Debug.Log("Looking at something new!");
                LookingAt = hit.collider.gameObject;
                // Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 60f);

            }
            else
            {
                // Debug.Log(hit.collider.gameObject.name);
                // Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 10f);
                LookingAt = null;
            }
            
        }
    }

    public void IncreaseEnergy()
    {
        // Debug.Log("Increase enegery");
        if (enegery < 100) { enegery += 1; }
    }

}
