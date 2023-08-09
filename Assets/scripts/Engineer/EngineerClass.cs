using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineerClass : MonoBehaviour
{
    public bool UltimateAvailable = true;
    public int enegery = 100;
    public int damage = 20;

    const string WALL_PREFAB_PATH = "Engineer/EngineerWall";

    private GameObject wallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        wallPrefab = Resources.Load<GameObject>(WALL_PREFAB_PATH);
        InvokeRepeating("IncreaseEnergy", 2.0f, 0.3f);
    }

    void Update()
    {
        if (wallPrefab == null)
        {
            wallPrefab = Resources.Load<GameObject>(WALL_PREFAB_PATH);
        }

        // Fortifcation Protcol
        if (Input.GetKeyDown(KeyCode.R) && UltimateAvailable && enegery >= 90)
        {

            PointGenerator point_gen = new PointGenerator();

            point_gen.player = this.gameObject;

            GameObject gen = (GameObject)Instantiate(Resources.Load<GameObject>("Engineer/EngineerGenerator"));

            Vector3 gen_pos = this.gameObject.transform.position;
            gen_pos.y += 2f;
            gen.transform.position = gen_pos;

            Wall gen_data = gen.GetComponent<Wall>();
            gen_data.generator = true;
            gen_data.decaying = true;

            var points = point_gen.GeneratePointsAroundPlayer(10f);

            for (int i = 0; i < points.Count; i++)
            {
                Vector3 point = points[i];
                GameObject wall = (GameObject)Instantiate(wallPrefab);
                wall.transform.position = point;
                Wall data = wall.GetComponent<Wall>();
                data.decaying = true;
            }

            enegery -= 90;
            
        }

        // Nanobot Rifle
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                EnemyLogic logic = hit.collider.gameObject.GetComponent<EnemyLogic>();
                if (logic != null )
                {
                    logic.health -= damage;
                    Debug.Log("Dealt Damage");
                }
                
            }
        }


    }

    public void IncreaseEnergy()
    {
        // Debug.Log("Increase enegery");
        if (enegery < 100) { enegery += 1; }
    }

}
