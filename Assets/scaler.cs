using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaler : MonoBehaviour
{
    private Camera minimapCam;
    private Transform t;
    private float startHeight;
    private float startScaleY;
    private float curHeight;
    private float objRatio;
    private float percentage,newY,newX;
    // Start is called before the first frame update
    void Start()
    {
        minimapCam = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        t = this.gameObject.transform;
        startHeight = minimapCam.orthographicSize * 2.0f;
        startScaleY = t.localScale.y;
        objRatio = t.localScale.x / t.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        curHeight = minimapCam.orthographicSize * 2.0f;
        percentage = ((curHeight - startHeight) / startHeight) + 1.0f;
        newY = startScaleY * percentage;
		newX = newY * objRatio;
        t.localScale = new Vector3(newX, newY, t.localScale.z);
    }
}
