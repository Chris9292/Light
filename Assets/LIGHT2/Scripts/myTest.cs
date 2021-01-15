using UnityEngine;
using System.Collections;

public class myTest : MonoBehaviour
{
    float time;
    WebCamTexture mycam = null;
    void Start()
    {
        time = 0f;
        WebCamDevice[] devices = WebCamTexture.devices;
        Debug.Log("Number of web cams connected: " + devices.Length);
        Renderer rend = this.GetComponentInChildren<Renderer>();

        mycam = new WebCamTexture();
        string camName = devices[0].name;
        Debug.Log("The webcam name is " + camName);
        mycam.deviceName = camName;
        rend.material.mainTexture = mycam;

        mycam.Play();

    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f)
        {
            mycam.Stop();
            time -= 3f;
        }
    }
}

