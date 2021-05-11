using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    //Reference to slider component of healthbar (fill element)
[SerializeField] private float val = 100 ;
[SerializeField] private float maxVal = 100;
[SerializeField] private Image metricImage = null;

[SerializeField] private Metrics metrics = null;


private float UpdateSpeed = 0.2f;

private float percent = 1f;

private void Start(){
    
    maxVal = metrics.maxHeartRate;
    val = maxVal;

}

private void Update(){
    

    
     //ReduceVal(20f);
     GetVal();
      StartCoroutine(ChangeVal(percent));
       
    
   
}

private void ReduceVal(float damage)
{
    val -= damage;
    percent = val/maxVal;


}

private void GetVal(){
    val = metrics.CalculateHeartRate();
    percent = val/metrics.maxHeartRate;
}

private IEnumerator ChangeVal(float percent)
{     
     float elapsed = 0f;
     float currPercent = metricImage.fillAmount;
   
    while(elapsed<UpdateSpeed)
    {
        elapsed += Time.deltaTime;
        metricImage.fillAmount = Mathf.Lerp(currPercent,percent,elapsed/UpdateSpeed); 

        yield return null;
    }
     metricImage.fillAmount = percent;

}





}
