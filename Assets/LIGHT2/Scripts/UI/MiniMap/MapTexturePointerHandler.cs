using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;


public class MapTexturePointerHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    private Camera MiniMapCamera = null;
    public float spawnHeight = 1f;

    private Camera mainCamera = null;
    private Vector3 startingDragPosition;
    public float DragSpeed = 0.2f;
    
    // TODO: more concept required
    public bool CanPlace { get; set; }
    
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
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Do nothing
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        if (CanPlace)
        {
            DONUT_Onclick();
        }
        else
        {
            // See below OnPointerDragged
            startingDragPosition = eventData.Pointer.Position;
        }
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        if (!CanPlace)
        {
            // Get the drag pointer position
            Vector3 dragPosition = eventData.Pointer.Position;

            // Get the drag direction relative to the main camera
            Vector3 tempDragDirection = mainCamera.transform.InverseTransformPoint(dragPosition) - mainCamera.transform.InverseTransformPoint(startingDragPosition);

            // Rotate the tempDragDirection according to a top down view and set its y to 0 (not sure if necessary) and normalize it
            // Better way would probably be to multiply with MiniMapCamera rotation
            Vector3 dragDirection = new Vector3(-tempDragDirection.x, 0f, -tempDragDirection.y).normalized;

            // Move the MiniMapCamera to drag direction
            MiniMapCamera.transform.position += (dragDirection * DragSpeed);

            // Set a new startingDragPoint
            startingDragPosition = dragPosition;
        }
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
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
        GameObject.Instantiate(Resources.Load("DO NUT"), spawnPosition, mainCamera.transform.rotation);

        // Talk about it
        Debug.Log("I was DOwN pUT here: " + spawnPosition.ToString("F3"));
    }
}
