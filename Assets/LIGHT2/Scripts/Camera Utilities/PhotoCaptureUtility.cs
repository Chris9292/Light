using UnityEngine;
using System.Linq;
using UnityEngine.Windows.WebCam;

public class PhotoCaptureUtility : MonoBehaviour
{
    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;

    // Public event. Gets called when a photo is taken or no resolutions are found and the mode is exited.
    public delegate void PhotoEventHandler();
    public event PhotoEventHandler OnPhotoModeEnded;

    public Texture2D TakePhoto()
    {
        // Check if there are any supported resolutions. Exit if none is found
        if (!PhotoCapture.SupportedResolutions.Any())
        {
            Debug.Log("No supported resolutions where found. Photo mode exited.");
            OnPhotoModeEnded?.Invoke();
            return null;
        }
            
        Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
        targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);

        // Create a PhotoCapture object
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = cameraResolution.width;
            cameraParameters.cameraResolutionHeight = cameraResolution.height;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {

                // Take a picture
                photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
                Debug.Log("Photo taken.");
            });
        });
        return targetTexture;
    }

    private void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        // Copy the raw image data into the target texture
        photoCaptureFrame.UploadImageDataToTexture(targetTexture);

        // Deactivate the camera
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    private void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        // Shutdown the photo capture resource
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
        OnPhotoModeEnded?.Invoke();
    }
}