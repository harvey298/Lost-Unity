using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Button connectbutton; // [SerializeField] private 
    public int test;


    private void Start()
    {
        // Add a click event handler to the button
        // connectbutton.clickable.clicked += OnMyButtonClick;
    }

    private void OnMyButtonClick()
    {
        // This method will be called when the button is clicked
        Debug.Log("Button clicked!");
        // Add your custom C# code here
    }

}
