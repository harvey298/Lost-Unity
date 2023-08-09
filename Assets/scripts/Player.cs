using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerClass
{
    Engineer = 0,
}

public class Player : MonoBehaviour
{
    public PlayerClass pClass = PlayerClass.Engineer;
    public GameObject engineerWall;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<EngineerClass>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}