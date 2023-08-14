using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remote_player : MonoBehaviour
{
    public PlayerClass pClass = PlayerClass.Engineer;
    public int health = 10;

    public GameObject player;

    // Effect, Time remaining
    // public Dictionary<Effects, int> effects = new Dictionary<Effects, int>();

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.position = player.transform.position;
        this.gameObject.transform.rotation = player.transform.rotation;

    }
}
