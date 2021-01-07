using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSource : MonoBehaviour
{
    [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.VideoRenderer videoRenderer;
    [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.WebcamSource webcamSource;
    
    public void StartVideoStream()
    {
        videoRenderer.StartRendering(webcamSource.Source);
    }

    
    public void StopVideoStream()
    {
        videoRenderer.StopRendering(webcamSource.Source);
    }
}
