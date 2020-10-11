using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.UI;

public class MyButtonInteraction : PressableButtonHoloLens2
{
    public UnityEvent OnMyButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        ButtonPressed.AddListener(MyMethod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MyMethod()
    {
        Debug.Log("aa");
    }
}
