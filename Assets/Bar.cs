using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TM_Pro;

public class Bar : MonoBehaviour
{
    //Reference to slider component of healthbar (fill element)
[SerializeField] private double val = 0 ;
[SerializeField] private double maxVal = 0;
[SerializeField] private Image metricImage = null;

[SerializeField] private Metrics metrics = null;

[SerializeField] private GameObject bar;

//[SerializeField] private TMP_Text valueDisplay; 


private float UpdateSpeed = 0.2f;

private double percent = 1f;

private void Start(){
    
    //maxVal = metrics.maxHeartRate;
    //Debug.Log(maxVal);
    string Name = bar.name; 
    Debug.Log(Name);
   switch (Name)
   {
       case "HeartRate":
        maxVal = metrics.maxHeartRate;
        break;
       case "Oxygen":
        maxVal = metrics.maxOxygenTime;
        break;
       case "MetabolicRate":
        maxVal = metrics.maxMetabolicRate;
        break;
       case "SuitPressure":
        maxVal = metrics.maxSuitPressure;
        break;
       default:
        maxVal = 100;
        break;
   }
    val = maxVal;

}

private void Update(){
    

    
     //ReduceVal(20f);
    GetVal();
    StartCoroutine(ChangeVal(percent));
    //valueDisplay.text = val;
       
    
   
}

private void ReduceVal(float damage)
{
    val -= damage;
    percent = val/maxVal;


}

private void GetVal(){
   // val = metrics.CalculateHeartRate();
   //string Name = bar.name; 
   switch (bar.name)
   {
       case "HeartRate":
        val = metrics.HeartRate;
        break;
       case "Oxygen":
        val = metrics.OxygenTime;
        break;
       case "MetabolicRate":
        val = metrics.MetabolicRate;
        break;
       case "SuitPressure":
        val = metrics.SuitPressure;
        break;
       default:
        val = 50;
        break;
   }
   // val = metrics.HeartRate;
    percent = val/maxVal;
}

private IEnumerator ChangeVal(double percent)
{     
     float elapsed = 0f;
     float currPercent = metricImage.fillAmount;
   
    while(elapsed<UpdateSpeed)
    {
        elapsed += Time.deltaTime;
        metricImage.fillAmount = Mathf.Lerp(currPercent,(float)percent,elapsed/UpdateSpeed); 

        yield return null;
    }
     metricImage.fillAmount = (float)percent;

}





}
