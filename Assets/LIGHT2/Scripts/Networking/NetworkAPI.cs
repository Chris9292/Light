using System;
using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
 
public static class NetworkAPI
{ 
    public static string url = "http://192.168.1.3:8000/api/sensor-data/";
    public static API_Response jsonResponse;
    
    public static IEnumerator GetSensorDataByName(string sensorName)
    {
        url = "http://192.168.1.3:8000/api/sensor-data/" + $"{sensorName}/";
        //responseTextMeshPro.text = $"Sending Http GET Request to following URL: {url}\n\n";
        
        var www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        Debug.Log(url);
        
        if (www.isHttpError || www.isNetworkError) {
            Debug.Log("Error: " + www.error);
            //responseTextMeshPro.text += $"Error: {www.error}\n";
        }
        else
        {
            try
            {
                jsonResponse = JsonConvert.DeserializeObject<API_Response>(www.downloadHandler.text);
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //responseTextMeshPro.text += $"HTTP status code: {www.responseCode}";
    }
    
}

