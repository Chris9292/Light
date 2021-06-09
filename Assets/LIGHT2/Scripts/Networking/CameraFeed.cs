using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFeed : MonoBehaviour
{
    public string url = "http://192.168.1.3:8000/api/camera/feed/";
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(CameraFeedCoroutine), 1, 0.05f);
    }

    void CameraFeedCoroutine()
    {
        StartCoroutine(nameof(DownloadImage));
    }
    
    IEnumerator DownloadImage()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture) www.downloadHandler).texture;
        }
    }
}
