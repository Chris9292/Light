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

    [Tooltip("Max Orthographic Size")]
    public float maxZoomOut = 6f;
    [Tooltip("Min Orthographic Size")]
    public float maxZoomIn = 3f;

    // The starting orthographic size of the camera. Used for AutoScaler
    public float DefaultOrthographicSize { get; private set; }
    public float CurrentOrthographicSize { get { return _miniMapCamera.orthographicSize; } }

    private void Awake()
    {
        mainCamera = Camera.main;
        _miniMapCamera = gameObject.GetComponent<Camera>();
        MustZoomIn = false;
        MustZoomOut = false;
        DefaultOrthographicSize = _miniMapCamera.orthographicSize;
    }

    private void OnEnable()
    {
        // Set this camera directly above the main camera
        transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
    }
    private void ZoomIn()
    {
        _miniMapCamera.orthographicSize -= ZoomSpeed;
    }

    private void ZoomOut()
    {
        _miniMapCamera.orthographicSize += ZoomSpeed;
    }
    private void Update()
    {
        if (MustZoomIn && _miniMapCamera.orthographicSize > maxZoomIn)
        {
            ZoomIn();
        }
        else if (MustZoomOut && _miniMapCamera.orthographicSize < maxZoomOut)
        {
            ZoomOut();
        }
    }
}
