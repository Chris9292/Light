using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject camera;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        offset = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = camera.transform.position + offset;
    }
}
