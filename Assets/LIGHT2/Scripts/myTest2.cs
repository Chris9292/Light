using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class myTest2 : MonoBehaviour
{
    TMP_Text tmp;

    private void Start()
    {

        tmp = GetComponentInChildren<TMP_Text>();
        Bounds textBounds = tmp.textBounds;

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Bounds bounds = mesh.bounds;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        int i = 0;
        while (i < uvs.Length)
        {
            uvs[i] = new Vector2(vertices[i].x / textBounds.size.x, vertices[i].z / textBounds.size.x);
            i++;
        }
        mesh.uv = uvs;

        mesh.bounds = textBounds;
    }
}
