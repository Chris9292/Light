using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircularMenuSelection))]
[RequireComponent(typeof(PhotoCaptureUtility))]

// A simple router showcasing the photoCapture component
public class PhotoSelectionRouter : MonoBehaviour
{
    // Initiate variables
    CircularMenuSelection c = null;
    PhotoCaptureUtility p = null;
    Texture2D photo = null;

    private void Start()
    {
        // Assign components to variables
        c = gameObject.GetComponent<CircularMenuSelection>();
        p = gameObject.GetComponent<PhotoCaptureUtility>();
     
        // Bind photo capture to click event
        c.OnButtonClicked += CaptureFrameToPhoto;
    }

    private void CaptureFrameToPhoto()
    {
        photo = p.TakePhoto();
        CreatePhotoQuad();
    }

    // Create a quad fixed at the camera at coordinates (1, 1, 7)
    private void CreatePhotoQuad()
    {
        // Create a GameObject to which the texture can be applied
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        Renderer quadRenderer = quad.GetComponent<Renderer>() as Renderer;
        quadRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard"));
        quadRenderer.material.SetTexture("_MainTex", photo);
        
        // Set the quad's parent to MainCamera
        quad.transform.parent = this.transform;
        quad.transform.SetParentWithTag("MainCamera");
        quad.transform.localPosition = new Vector3(1.0f, 1.0f, 7.0f);
    }
}