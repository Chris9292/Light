using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
 
public class WebRequest: MonoBehaviour
{

    
    public string url;
    public TextMeshProUGUI responseTextMeshPro;

    void Start()
    {
        responseTextMeshPro.text = $"Sending Http Request to following URL: {url}\n\n";
    }
    public void NetworkAPI()
    {
        StartCoroutine(GetServerData());
    }
 
    private IEnumerator GetServerData()
    {
        responseTextMeshPro.text = $"Sending Http Request to following URL: {url}\n\n";
        
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError) {
            Debug.Log("Error: " + www.error);
            responseTextMeshPro.text += $"Error: {www.error}";
        }
        else {
            
            var jsonResponse = JsonConvert.DeserializeObject<API_Response>(www.downloadHandler.text);
            
            responseTextMeshPro.text += $"ID : {jsonResponse.id}" +"\n";
            responseTextMeshPro.text += $"S_ID : {jsonResponse.s_id}" + "\n";
            responseTextMeshPro.text += $"Name : {jsonResponse.name}" +"\n";
            responseTextMeshPro.text += $"Value : {jsonResponse.value}" +"\n";
            responseTextMeshPro.text += $"Date : {jsonResponse.date}" +"\n\n";
            responseTextMeshPro.text += $"HTTP status code: {www.responseCode}";
        }
    }
    
}