using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorClass : MonoBehaviour
{
    private int ultimateCooldownRemaining = 0;

    private int altFireCooldownRemaining = 0;

    private int Ability2CooldownRemaining = 0;

    public int enegery = 100;
    public int damage = 20;

    // Probs bad for naming, but it only effects me, need to change the naming
    const string LEGION = "Emperor/EmperorLegion";

    const string SOLDIER = "Emperor/EmperorPortal";

    private GameObject legionPrefab;

    private GameObject LookingAt;

    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {

        legionPrefab = Resources.Load<GameObject>(LEGION);
        InvokeRepeating("IncreaseEnergy", 2.0f, 0.3f);
        InvokeRepeating("decrease_cool_downs", 2.0f, 1f);

        cam = Camera.main;

    }

    private void decrease_cool_downs()
    {
        if (ultimateCooldownRemaining < 0) { ultimateCooldownRemaining = 0; }
        if (ultimateCooldownRemaining > 0) { ultimateCooldownRemaining -= 1; }

        if (altFireCooldownRemaining < 0) { altFireCooldownRemaining = 0; }
        if (altFireCooldownRemaining > 0) { altFireCooldownRemaining -= 1; }

        if (Ability2CooldownRemaining < 0) { Ability2CooldownRemaining = 0; }
        if (Ability2CooldownRemaining > 0) { Ability2CooldownRemaining -= 1; }
    }

    void Update()
    {
        if (legionPrefab == null)
        {
            legionPrefab = Resources.Load<GameObject>(LEGION);
        }

        Player thisPlayer = this.gameObject.GetComponent<Player>();

        // Emperor's Greed/Imperial Conquest - Applies Subjugate or Conquer to enemies
        // if not enemy is in LOS then apply Conquer, if a single enemy is in LOS then apply Subjugate
        if (Input.GetKeyDown(KeyCode.R) && ultimateCooldownRemaining == 0 && enegery >= 90)
        {
            ultimateCooldownRemaining = 15;

            var activated = false;
            if (LookingAt != null)
            {
                var logic = LookingAt.GetComponent<EnemyLogic>();
                if (logic != null)
                {
                    logic.add_effect(Effects.Subjugate, 30);
                    activated = true;
                }
            }

            if (!activated)
            {
                Vector3 position = this.gameObject.transform.position;

                Collider[] hitColliders = Physics.OverlapSphere(position, 10f);
                foreach (var hitCollider in hitColliders)
                {
                    var enemy = hitCollider.GetComponent<EnemyLogic>();
                    if (enemy != null && enemy.gameObject != null)
                    {
                        enemy.add_effect(Effects.Conquer, 30);
                        activated = true;
                    }
                }
            }

            if (activated)
            {
                thisPlayer.add_effect(Effects.Tryant, 30);
            }

            // Spawn the legion (the wall)
            Vector3 spawnPos = this.transform.position;
            spawnPos.z += 1;

            GameObject legion = Instantiate(Resources.Load<GameObject>("Emperor/EmperorLegion"));

            legion.transform.position = spawnPos;
            legion.transform.rotation = this.transform.rotation;
            
            EmperorLegion LegionLogic = legion.GetComponent<EmperorLegion>();
            LegionLogic.owner = this.GetComponent<Player>();

            LegionLogic.Horizontal = Input.GetAxis("Horizontal");
            LegionLogic.Vertical = Input.GetAxis("Vertical");
            LegionLogic.originialRotation = this.transform.rotation;

        }

        // Imperial Legion (soliders)
        if (Input.GetKeyDown(KeyCode.LeftShift) && enegery >= 50 && Ability2CooldownRemaining == 0)
        {
            var soliderSpawner = Instantiate(Resources.Load<GameObject>(SOLDIER));
            soliderSpawner.transform.position = this.transform.position;
            soliderSpawner.GetComponent<EmperorLegionPortal>().owner = this.GetComponent<Player>(); // TODO: Cache the player
            Ability2CooldownRemaining = 35; // TODO: Increase this?

            // enegery -= 50;
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

            }
            else
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
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
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
