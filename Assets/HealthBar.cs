using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Reference to slider component of healthbar (fill element)
[SerializeField] private float val ;
[SerializeField] private float maxVal;
[SerializeField] private Image metricImage;

private float UpdateSpeed = 0.2f;

private float percent = 1f;

private void Update(){
    

    if(Input.GetKeyDown("e")){
       //val -= 10f;
       ReduceVal(20f);
       StartCoroutine(ChangeVal(percent));
       
    }
   
}

private void ReduceVal(float damage)
{
    val -= damage;
    percent = val/maxVal;


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
