using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerClass
{
    Chairman = -1, // an intangable player, the Assistant director
    Engineer = 0, // util class
    Emperor = 1, //  high damage over time class
}

public class Player : MonoBehaviour
{
    public PlayerClass pClass = PlayerClass.Emperor;
    public GameObject engineerWall;

    public int MaxHealth = 50;
    public int health;

    // Effect, Time remaining
    public Dictionary<Effects, int> effects = new Dictionary<Effects, int>();

    // Start is called before the first frame update
    void Start()
    {
        //if (this.GetComponent<PlayerNetworkManager>().IsServer)
        {
          //  pClass = PlayerClass.Chairman;
        }

        health = MaxHealth;

        pClass = PlayerClass.Emperor;

        GameObject obj = GameObject.FindGameObjectWithTag("GUI");
        var class_text = obj.GetComponent<TextMeshProUGUI>();

        // Add the class component
        if ((int)pClass == -1)
        {
            this.gameObject.AddComponent<ChairMan>();
            class_text.text = "ChairMan";

        } else if ((int)pClass == 0)
        {
            this.gameObject.AddComponent<EngineerClass>();
            class_text.text = "Engineer";
        }
        else if ((int)pClass == 1)
        {
            this.gameObject.AddComponent<EmperorClass>();
            class_text.text = "Emperor";
        }



    }

    public void ChangeClass(PlayerClass NewPClass)
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GUI");
        var class_text = obj.GetComponent<TextMeshProUGUI>();

        // Remove old class traits
        int pClassInt = (int)pClass;
        if (pClassInt == -1)
        {
            Destroy(this.gameObject.AddComponent<ChairMan>());
        }
        else if (pClassInt == 0)
        {
            Destroy(this.gameObject.AddComponent<EngineerClass>());
        }
        else if (pClassInt == 1)
        {
            Destroy(this.gameObject.AddComponent<EmperorClass>());
        }

        // Add new class
        pClass = NewPClass;
        if ((int)pClass == -1)
        {
            this.gameObject.AddComponent<ChairMan>();
            class_text.text = "ChairMan";

        }
        else if ((int)pClass == 0)
        {
            this.gameObject.AddComponent<EngineerClass>();
            class_text.text = "Engineer";
        }
        else if ((int)pClass == 1)
        {
            this.gameObject.AddComponent<EmperorClass>();
            class_text.text = "Emperor";
        }


    }


    public void add_effect(Effects effect, int length)
    {
        effects.Add(effect, length);
        Debug.Log(effects);
    }

    // Update is called once per frame
    void Update()
    {

    }
}