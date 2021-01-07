using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveSource : MonoBehaviour
{
    [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.VideoRenderer videoRenderer;
    [SerializeField] private Microsoft.MixedReality.WebRTC.Unity.VideoReceiver videoReceiver;
    public void StartRender()
    {
        videoRenderer.StartRendering(videoReceiver.VideoTrack);
    }

    // Update is called once per frame
    public void StopRender()
    {
        videoRenderer.StopRendering(videoReceiver.VideoTrack);
    }
}
