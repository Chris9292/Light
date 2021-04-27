using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    public Texture2D mainTex;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetTexture("_MainTex", mainTex);
    }
}
