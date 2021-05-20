using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bar : MonoBehaviour
{
    private double val = 0;
    private double maxVal = 0;    
    
    //Reference to slider component of healthbar (fill element)
    [SerializeField] private Image metricImage = null;

    [SerializeField] private Metrics metrics = null;

    public TMP_Text valueDisplay;
    public string metricUnits;


    private float LerpUpdateSpeed = 0.2f;

    private double percent = 1f;

    private void Start()
    {

        //maxVal = metrics.maxHeartRate;
        //Debug.Log(maxVal);
        string Name = gameObject.name;
        Debug.Log(Name);
        switch (Name)
        {
            case "Heart Rate":
                maxVal = metrics.maxHeartRate;
                break;
            case "Oxygen Supply":
                maxVal = metrics.maxOxygenTime;
                break;
            case "Metabolic Rate":
                maxVal = metrics.maxMetabolicRate;
                break;
            case "Suit Pressure":
                maxVal = metrics.maxSuitPressure;
                break;
            case "Temperature":
                maxVal = metrics.maxTemperature;
                break;
            case "Humidity":
                maxVal = metrics.maxHumidity;
                break;
            case "Radiation":
                maxVal = metrics.maxRadiation;
                break;
            default:
                maxVal = 100;
                break;
        }
        val = maxVal;

    }

    private void Update()
    {
        GetVal();
        StartCoroutine(ChangeVal(percent));
        valueDisplay.text = val.ToString() + " " + metricUnits;
        
        /*if (gameObject.name == "Radiation")
        {
            Debug.Log(val);
        }*/
    }

    private void GetVal()
    {
        switch (gameObject.name)
        {
            case "Heart Rate":
                val = metrics.HeartRate;
                break;
            case "Oxygen Supply":
                val = metrics.OxygenTime;
                break;
            case "Metabolic Rate":
                val = metrics.MetabolicRate;
                break;
            case "Suit Pressure":
                val = metrics.SuitPressure;
                break;
            case "Temperature":
                val = metrics.Temperature;
                break;
            case "Humidity":
                val = metrics.Humidity;
                break;
            case "Radiation":
                val = metrics.Radiation;
                break;
            default:
                val = 50.50;
                break;
        }

        percent = val / maxVal;
    }

    private IEnumerator ChangeVal(double percent)
    {
        float elapsed = 0f;
        float currPercent = metricImage.fillAmount;

        while (elapsed < LerpUpdateSpeed)
        {
            elapsed += Time.deltaTime;
            metricImage.fillAmount = Mathf.Lerp(currPercent, (float)percent, elapsed / LerpUpdateSpeed);

            yield return null;
        }
        metricImage.fillAmount = (float)percent;
    }
}