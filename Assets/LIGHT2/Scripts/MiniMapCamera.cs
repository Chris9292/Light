using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MiniMapCamera : MonoBehaviour
{
    // gameObject's Camera Component
    private Camera camera = null;
    // The ZoomIn/ZoomOut speed
    public float ZoomSpeed = 0.06f;
    // When set to true camera will ZoomIn in each update
    public bool MustZoomIn { get; set; }
    // When set to true camera will ZoomOut in each update
    public bool MustZoomOut { get; set; }

    private void Awake()
    {
        camera = gameObject.GetComponent<Camera>();
        MustZoomIn = false;
        MustZoomOut = false;
    }
    private void ZoomIn()
    {
        camera.orthographicSize -= ZoomSpeed;
    }

    private void ZoomOut()
    {
        camera.orthographicSize += ZoomSpeed;
    }
    private void Update()
    {
        if (MustZoomIn)
        {
            ZoomIn();
        }
        else if (MustZoomOut)
        {
            ZoomOut();
        }
    }
}
