using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmperorLegionPortal : MonoBehaviour
{
    private GameObject UnitPrefab;
    public int UnitsToSpawn = 5;
    public Player owner;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnUnit", 2.0f, 1f);
        UnitPrefab = Resources.Load<GameObject>("Emperor/EmperorSoldier");
    }

    private void SpawnUnit()
    {
        if (UnitsToSpawn <= 0)
        {
            Destroy(this.gameObject);
            return;
        }

        var unit = Instantiate(UnitPrefab);
        var logic = unit.GetComponent<EmperorSoldier>();

        logic.owner = this.owner;
        UnitsToSpawn -= 1;
    }

}
