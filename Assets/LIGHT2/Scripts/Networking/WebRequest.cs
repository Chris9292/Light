using System;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
 
public class WebRequest: MonoBehaviour
{
    
    public static string url = "http://127.0.0.1:8000/api/sensor-data/";
    public TextMeshProUGUI responseTextMeshPro;

    void Start()
    {
        responseTextMeshPro.text = $"Sending Http GET Request to following URL: {url}\n\n";
    }
    public void NetworkAPI()
    {
        StartCoroutine(GetServerData());
    }
 
    private IEnumerator GetServerData()
    {
        responseTextMeshPro.text = $"Sending Http GET Request to following URL: {url}\n\n";
        
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isHttpError || www.isNetworkError) {
            Debug.Log("Error: " + www.error);
            responseTextMeshPro.text += $"Error: {www.error}\n";
        }
        else {

            try
            {
                var jsonResponse = JsonConvert.DeserializeObject<API_Response>(www.downloadHandler.text);
                responseTextMeshPro.text += $"ID : {jsonResponse.id}" +"\n";
                responseTextMeshPro.text += $"S_ID : {jsonResponse.s_id}" + "\n";
                responseTextMeshPro.text += $"Name : {jsonResponse.name}" +"\n";
                responseTextMeshPro.text += $"Value : {jsonResponse.value}" +"\n";
                responseTextMeshPro.text += $"Date : {jsonResponse.date}" +"\n\n";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                responseTextMeshPro.text += $"Exception : {e.Message}\n";
            }
            
            

        }
        responseTextMeshPro.text += $"HTTP status code: {www.responseCode}";
    }
}