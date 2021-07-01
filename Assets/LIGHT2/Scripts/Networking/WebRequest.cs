using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
 
public class WebRequest: MonoBehaviour
{
    //private UnityWebRequest www;
    public static string url = "http://192.168.1.100:8000/api/sensor-data/";
    public TextMeshProUGUI responseTextMeshPro;
    
    private void Start()
    {
        responseTextMeshPro.text = $"Sending Http GET Request to following URL: {url}\n\n";
        //InvokeRepeating(nameof(NetworkAPI), 2, 2);
    }


    public void NetworkTester()
    {
        StartCoroutine((nameof(GetSensorDataByName)));
    }
    
    private IEnumerator GetSensorDataByName(string sensorName)
    {

        url = "http://192.168.1.100:8000/api/sensor-data/" + $"{sensorName}/";
        responseTextMeshPro.text = $"Sending Http GET Request to following URL: {url}\n\n";
        
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        Debug.Log(url);
        if (www.isHttpError || www.isNetworkError) {
            Debug.Log("Error: " + www.error);
            responseTextMeshPro.text += $"Error: {www.error}\n";
        }
        else {

            try
            {
                var jsonResponse = JsonConvert.DeserializeObject<API_Response>(www.downloadHandler.text);
                Debug.Log(www.downloadHandler.text);
                responseTextMeshPro.text += $"ID : {jsonResponse.id}" +"\n";
                responseTextMeshPro.text += $"Name : {jsonResponse.name}" +"\n";
                responseTextMeshPro.text += $"Value : {jsonResponse.value}" +"\n";
                responseTextMeshPro.text += $"Date : {jsonResponse.date}" +"\n\n";
            }
            
            catch (Exception e)
            {
                Debug.Log(e);
                responseTextMeshPro.text += $"Exception : {e.Message}\n";
            }
        }
        responseTextMeshPro.text += $"HTTP status code: {www.responseCode}";
        
    }
}
