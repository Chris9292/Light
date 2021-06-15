using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFeed : MonoBehaviour
{
    public string url = "http://192.168.1.3:8000/api/camera/feed/";

    // public targetTexture = GetComponent<Renderer>().material.mainTexture
    public Renderer targetRenderer;
    private bool requestDone = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(DownloadImage));
        InvokeRepeating(nameof(CameraFeedCoroutine), 1, 1.0f);
    }

    void CameraFeedCoroutine()
    {
        if (requestDone)
        {
            StartCoroutine(nameof(DownloadImage));    
        }
    }
    
    IEnumerator DownloadImage()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        requestDone = true;
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            targetRenderer.material.mainTexture = myTexture;
        }
        www.Dispose();
    }
}
