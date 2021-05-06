using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    public Color color;
    void Start()
    {
        GetComponent<Renderer>().material.color = color;
    }
}
