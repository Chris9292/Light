using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircularMenuSelection))]
[RequireComponent(typeof(PhotoCaptureExample))]
public class myPhotoSelection : MonoBehaviour
{
    private void Start()
    {
        CircularMenuSelection c = new CircularMenuSelection();
        c = gameObject.GetComponent<CircularMenuSelection>();

        PhotoCaptureExample p = new PhotoCaptureExample();
        p = gameObject.GetComponent<PhotoCaptureExample>();
     
        c.OnButtonClicked += delegate () { Debug.Log("hi"); };
        c.OnButtonClicked += p.TakePhoto;
    }
}