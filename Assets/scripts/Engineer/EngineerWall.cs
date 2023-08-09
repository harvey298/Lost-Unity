using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject obj;
    public int decay_time = 1000;
    public bool decaying = false;
    public bool generator = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!decaying) { return; }

        decay_time -= 1;

        if (decay_time <= 0)
        {
            Destroy(obj);
            return;
        }

        if (generator)
        {
            Debug.Log("TODO: engineer generator");
        }


    }
}
