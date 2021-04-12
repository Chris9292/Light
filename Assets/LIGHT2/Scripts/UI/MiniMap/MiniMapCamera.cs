using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MiniMapCamera : MonoBehaviour
{
    private Camera mainCamera = null;
    // gameObject's Camera Component
    private Camera _miniMapCamera = null;
    // The ZoomIn/ZoomOut speed
    public float ZoomSpeed = 0.06f;
    // When set to true camera will ZoomIn in each update
    public bool MustZoomIn { get; set; }
    // When set to true camera will ZoomOut in each update
    public bool MustZoomOut { get; set; }

    public float minSize = 4.0f;
    public float maxSize = 16.0f;

    private void Awake()
    {
        mainCamera = Camera.main;
        _miniMapCamera = gameObject.GetComponent<Camera>();
        MustZoomIn = false;
        MustZoomOut = false;
    }

    private void OnEnable()
    {
        // Set this camera directly above the main camera
        transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }
    private void ZoomIn()
    {
        _miniMapCamera.orthographicSize = ((_miniMapCamera.orthographicSize - ZoomSpeed) > minSize)?
                                                _miniMapCamera.orthographicSize - ZoomSpeed : minSize;
    }

    private void ZoomOut()
    {
        _miniMapCamera.orthographicSize = ((_miniMapCamera.orthographicSize + ZoomSpeed) < maxSize) ?
                                                _miniMapCamera.orthographicSize + ZoomSpeed : maxSize;
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
