using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: name polish
public class MySelection : MonoBehaviour
{
    public delegate void myButtonClickedHandler();
    public event myButtonClickedHandler ButtonClicked;

    public void selected()
    {
        Debug.Log("hey");
        if (ButtonClicked != null)
        {
            ButtonClicked();
        }
    }

    private void Start()
    {
        gameObject.tag = "CircularMenuSelection";
        ButtonClicked += () => { Debug.Log("click simulation !"); };
    }
}