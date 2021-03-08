using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

// TODO: Make it work
public class CameraReferencePoint : MonoBehaviour
{
    private Camera mainCamera = null;
    public float Altitude = 10f;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        // Set this transform directly above the main camera keeping its original altitude
        transform.position = new Vector3(mainCamera.transform.position.x, Altitude, mainCamera.transform.position.z);
    }

    public void UpdateRotation(ManipulationEventData data)
    {
        transform.rotation = data.ManipulationSource.transform.rotation;
    }
}