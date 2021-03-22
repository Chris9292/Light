using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public MeshRenderer rend;
    public Material[] mats;
    public int cur;

    private void Start()
    {
        if (rend == null)
        {
            rend = GetComponent<MeshRenderer>();
        }
    }

    public void RandomColor()
    {
        rend.material.color = UnityEngine.Random.ColorHSV();
    }
}
