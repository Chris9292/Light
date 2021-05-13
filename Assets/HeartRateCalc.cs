using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateCalc : MonoBehaviour
{

    public double HeartRate;  //  bpm 
    public double maxHeartRate ;
    public double avgHeartRate = 90; 
    public bool IsCalm;
    // Start is called before the first frame update
    void Start()
    {
        IsCalm = true; 
        maxHeartRate = 160;
    }

    // Update is called once per frame
    void Update()
    {
        //CalculateHeartRate();
        
        //Debug.Log(HeartRate);
    }

    public void CalculateHeartRate()       //avg: 60-100 , range: 60-160
    {
        if(IsCalm)
        {
           //HeartRate = avgHeartRate + Mathf.Cos(Random.Range(-5,5));
           HeartRate = avgHeartRate + Random.Range(-5,5);
        }

        //HeartRate = avgHeartRate + 50 + Mathf.Cos(Random.Range(-5,5));
         HeartRate = avgHeartRate + Random.Range(-5,5);


    }
}
