using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTemplate : MonoBehaviour
{
    public bool UltimateAvailable = true;
    public int enegery = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && UltimateAvailable && enegery >= 90)
        {


            enegery -= 90;
            
        }

        if (enegery < 100) { enegery += 1; }
    }
}
