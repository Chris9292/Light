using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myTest2 : MonoBehaviour
{
    //Texture2D targetTexture = null;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard"));

        quad.transform.parent = this.transform;
        quad.transform.localPosition = new Vector3(0.7f, 0.4f, 5.0f);

        //quadRenderer.material.SetTexture("_MainTex", targetTexture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
