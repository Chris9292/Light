using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour
{
   //Grid GridReference;
    private float  timeElapsed;
    private double distanceTraversed;
    private double distance;
    private double velocity;  // m/s
    
    [SerializeField] private float slope = 0;      // degrees
    [SerializeField] private double mass = 0;       // Kg
    [SerializeField] private double g = 0;    // Kg/s


    //Biometrics
    private double MetabolicRate; //  Joule/m

    private double HeartRate; //  bpm
    private double maxHeartRate;
    private double avgHeartRate = 90;
    private bool IsCalm;

    private float OxygenTime; // minutes to oxygen depletion
    private float depletionRate = 1;
    private bool timerIsRunning;  //Timer control

    private double SuitPressure;

    //Environment Metrics

 


    private void Awake()
    {
        //GridReference = GetComponent<Grid>;  
        //slope = 1; //degrees 
        timerIsRunning = true;
        IsCalm = true; 

    }

  
    void Update()
    {  
        //CalculateVelocity(slope);
       // Debug.Log(velocity);
       // CalculateMetabolicRate(slope);
        //Debug.Log(MetabolicRate);
        CalculateHeartRate();
        //Debug.Log(HeartRate);

    }

        //Approximate astronaut velocity and metabolic rate relations (Santee et al.2001)

    private void CalculateVelocity(float slope) 
    {
       if(slope< -20 || slope > 20)
        velocity = 0;
       else if(slope <= -10)
            velocity = 0.095*slope + 1.95;
       else if(slope <0)
            velocity = 0.05*slope + 1.6;
       else if(slope<6)
            velocity = -0.2*slope + 1.6;
       else if(slope<15)
            velocity = 0.039*slope + 0.634;
       else
            velocity = 0.05;
       
       

    }

    private void CalculateMetabolicRate(float slope)
    {  
        float slope_sin = Mathf.Sin(slope);
        float slope_cos = Mathf.Cos(slope);
        float slope_abs = Mathf.Abs(slope);

        double LevelEnergy = (3.28 * mass + 71.1)*(0.661* velocity * slope_cos + 0.115); 
        double SlopeEnergy ; 
        if(slope > 0)
            SlopeEnergy = 3.5 * mass * g * velocity * slope_sin;
        else if(slope < 0)
            SlopeEnergy = 2.4 * mass * g * velocity * slope_sin * Mathf.Pow(0.3f,(float)(slope_abs/7.65));
        else 
           { 
               SlopeEnergy = 0;
           }

           
      MetabolicRate = LevelEnergy + SlopeEnergy;
        
    }

    private void CalculateOxygen(){
        if(timerIsRunning){
            if (OxygenTime > 0 )
            {
                OxygenTime -= depletionRate * Time.deltaTime;
            }
            else 
            {
                OxygenTime = 0;
                timerIsRunning = false;

            }

        }

    }

    private void CalculateHeartRate()       //avg: 60-100 , range: 60-160
    {
        if(IsCalm)
        {
           HeartRate = avgHeartRate + Mathf.Cos(Random.Range(-5,5));
        }

        HeartRate = avgHeartRate + 50 + Mathf.Cos(Random.Range(-5,5));


    }

    private void CalculateSuitPress(){

    }

   // int GetManhattanDist(Node nodeA,Node nodeB){
       // int ix = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);//x1-x2
       // int iy = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);//y1-y2

       // return ix + iy;//Return the sum
   // }

}
