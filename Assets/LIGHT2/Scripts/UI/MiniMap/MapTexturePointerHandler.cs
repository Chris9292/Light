using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Threading.Tasks;
using UnityEngine;


public class MapTexturePointerHandler : MonoBehaviour, IMixedRealityPointerHandler
{
    private Camera MiniMapCamera = null;
    public float spawnHeight = 1f;

    private void Start()
    {
        // TODO: Error handling
        MiniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera").GetComponent<Camera>();
    }
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        // Do nothing
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData)
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

        // TODO: 1st created sphere lags as hell (might have to do with Resources.Load)
        new Task(() => { CreateSphere(spawnPosition); }).Start();

        Debug.Log("I was put down here: " + spawnPosition.ToString("F3"));
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        // TODO: Move map arround
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        // Do nothing
    }

    private void CreateSphere(Vector3 spawnPosition)
    {
        Texture2D photo = Resources.Load < Texture2D > ("chart");
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Renderer sphereRenderer = sphere.GetComponent<Renderer>() as Renderer;
        sphereRenderer.material = new Material(Shader.Find("Mixed Reality Toolkit/Standard"));
        sphereRenderer.material.SetTexture("_MainTex", photo);

        sphere.transform.position = spawnPosition;
        sphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}
