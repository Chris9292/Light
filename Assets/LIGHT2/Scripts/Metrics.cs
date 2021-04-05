using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metrics : MonoBehaviour
{
   //Grid GridReference;
    private double distance;
    private double velocity;  // m/s
    
    [SerializeField] private float slope;      // degrees
    [SerializeField] private double mass;       // Kg
    [SerializeField] private double g;    // Kg/s


    //Biometrics
    private double MetabolicRate; //  Joule/m
    private double HeartRate; //  bpm
    private double Oxygen; // minutes to end
    private double SuitPressure;

    //Environment Metrics


    private void Awake()
    {
        //GridReference = GetComponent<Grid>;  
        //slope = 1; //degrees 

    }

  
    void Update()
    {  
         CalculateVelocity(slope);
       // Debug.Log(velocity);
        CalculateMetabolicRate(slope);
        Debug.Log(MetabolicRate);
    }

    
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


   // int GetManhattanDist(Node nodeA,Node nodeB){
       // int ix = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);//x1-x2
       // int iy = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);//y1-y2

       // return ix + iy;//Return the sum
   // }

}
