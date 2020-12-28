using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

/// <summary>
/// The map has 2 modes. Either movement by dragging or returning/placing world coordinates by clicking.
/// </summary>
public class MapTexturePointerHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    // Cameras
    private Camera MiniMapCamera = null;
    private Camera mainCamera = null;

    // The height where the objects will be placed when clicking the MiniMap
    public float spawnHeight = 0f;

    // When set to true the MiniMap is in placing mode
    // When set to false the MiniMap is in moving mode
    public bool CanPlace { get; set; }


    // Move logic

    // The camera movement speed
    public float DragSpeed = 0.3f;

    // The time spent lerping
    public float LerpTime = 0.01f;

    // The hand movement threshold in meters to apply any kind of movement
    public float DragThreshold = 0.008f;
    
    private Vector3 StartDragPosition;
    private Vector3 EndDragPosition;

    private bool IsLerping = false;
    private float TimeStartedLerping = 0f;

    private Vector3 StartLerpPosition;
    private Vector3 EndLerpPosition;

    // A tasty donut
    private GameObject DoNut;

    private void Awake()
    {
        DoNut = Resources.Load("DO NUT") as GameObject;
    }
    private void OnEnable()
    {
        CanPlace = false;
    }

    private void Start()
    {
        try
        {
            MiniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
        }
        catch (UnityException)
        {
            throw new UnityException("Failed to get MiniMapCamera");
        }

        try
        {
            mainCamera = Camera.main;
        }
        catch (UnityException)
        {
            throw new UnityException("Failed to get MainCamera");
        }     
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (CanPlace)
        {
            DONUT_Onclick();
        }
        else
        {
            // Set the starting drag position for our move
            StartDragPosition = eventData.Pointer.Position;
        }
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (!CanPlace)
        {
            // Get the drag pointer position
            EndDragPosition = eventData.Pointer.Position;
      
            // Start moving only when exceeding a given threshold to make the object more stable
            if (Vector3.Distance(StartDragPosition, EndDragPosition) > DragThreshold)
            {
                StartLerping();
                StartDragPosition = EndDragPosition;
            }
        }   
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Do nothing
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Do nothing
    }

    private void DONUT_Onclick()
    {
        // See below
        float offset = 0.5f;

        // Get the global position of the hit surface
        Vector3 cursorPosition = CoreServices.InputSystem.GazeProvider.HitInfo.point;

        // Convert the global position to local position (1x1 surface with the center(0,0) in the middle)
        Vector3 targetPosition = transform.InverseTransformPoint(cursorPosition);

        // Create a pixel Vector3 to pass into the ScreenToWorldMethod
        // The (0,0) point must be in the bottom left corner so we add the offset which for a 1x1 object is 0.5 and we multiply by the pixel width and height
        // Furthermore, the height is defined as the distance from the camera so since we have a top-down camera we define the height as camera.y - spawn height
        Vector3 pixelPosition = new Vector3((targetPosition.x + offset) * 700, (targetPosition.y + offset) * 500, MiniMapCamera.gameObject.transform.position.y - spawnHeight);

        // Get the global spawn position
        Vector3 spawnPosition = MiniMapCamera.ScreenToWorldPoint(pixelPosition);

        // Instantiate a DONUT
        GameObject.Instantiate(DoNut, spawnPosition, mainCamera.transform.rotation);

        // Talk about it
        Debug.Log("I was DOwN pUT here: " + spawnPosition.ToString("F2"));
    }

    // Begin our lerping motion
    private void StartLerping()
    {
        IsLerping = true;
        TimeStartedLerping = Time.time;


        // Get the drag direction relative to the main camera
        Vector3 tempDragDirection = mainCamera.transform.InverseTransformPoint(EndDragPosition) - mainCamera.transform.InverseTransformPoint(StartDragPosition);

        // Rotate the tempDragDirection according to a top down view and set its y to 0 (not sure if necessary) and normalize it
        // Better way would probably be to multiply with MiniMapCamera rotation
        Vector3 dragDirection = new Vector3(-tempDragDirection.x, 0f, -tempDragDirection.y).normalized;

        StartLerpPosition = MiniMapCamera.transform.position;
        EndLerpPosition = MiniMapCamera.transform.position + (dragDirection * DragSpeed);
    }

    private void FixedUpdate()
    {
        // If we are lerping apply the new lerp position in each frame
        if (IsLerping)
        {
            float timeSinceStarted = Time.time - TimeStartedLerping;
            float percentageComplete = timeSinceStarted / LerpTime;

            MiniMapCamera.transform.position = Vector3.Lerp(StartLerpPosition, EndLerpPosition, percentageComplete);

            if (percentageComplete >= 1f)
            {
                IsLerping = false;
            }
        }
    }
}