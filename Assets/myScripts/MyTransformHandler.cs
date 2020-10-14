using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;

public class MyTransformHandler : MonoBehaviour
{
    public void SetToParentCoords() 
    {
        transform.position = transform.parent.position;
    }
}
