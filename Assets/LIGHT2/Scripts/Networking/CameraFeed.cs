using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class CameraFeed : MonoBehaviour
{
    private string url = "http://192.168.1.100:8000/api/camera/feed/";

    private Texture2D tex;
    // public targetTexture = GetComponent<Renderer>().material.mainTexture
    public Renderer targetRenderer;
    //private bool requestDone = false;
    // Start is called before the first frame update
    void Start()
    {
        tex = new Texture2D(2, 2);
        InvokeRepeating("CameraFeedCoroutine", 1, 0.1f);
    }

    void CameraFeedCoroutine()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(nameof(DownloadImage));
        }
    }
    
    IEnumerator DownloadImage()
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(url))
        {
            //Texture2D tex = new Texture2D(2, 2);
            yield return uwr.SendWebRequest();

            //requestDone = true;
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log("Error: " + uwr.error);
            }
            else
            {
                //Texture myTexture = DownloadHandlerTexture.GetContent(uwr);
                tex.LoadImage(uwr.downloadHandler.data);
                targetRenderer.material.mainTexture = tex;
            }
        }
    }
}
