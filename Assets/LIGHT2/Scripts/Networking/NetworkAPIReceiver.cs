using System.Collections;
using UnityEngine;

public class NetworkAPIReceiver : MonoBehaviour
{

    public string sensorName;
    public double tempValue;
    public double radiationValue;
    public double humidityValue;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(TestApi), 2, 2);
    }

    private void TestApi()
    {
        StartCoroutine(NetworkAPI.GetSensorDataByName("Temperature[*C]"));
        StartCoroutine(NetworkAPI.GetSensorDataByName("radiation"));
        StartCoroutine(NetworkAPI.GetSensorDataByName("Humidity"));
    }
    
    void Update()
    {
        var sensorName = NetworkAPI.jsonResponse.name;
        var value = NetworkAPI.jsonResponse.value;
        
        switch (sensorName)
        {
            case "radiation":
                radiationValue = value;
                break;
            case "Humidity":
                humidityValue = value;
                break;
            case "Temperature[*C]":
                tempValue = value;
                break;
        }
    }
}
