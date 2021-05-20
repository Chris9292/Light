using UnityEngine;

public class Metrics : MonoBehaviour
{
    
    private double velocity;  // m/s

    ///Biometrics
    
    // Constants
    public double avgHeartRate = 90;
    public float depletionRate = 1;
    [SerializeField] private float slope = 0;      // degrees
    [SerializeField] private double mass = 0;       // Kg
    [SerializeField] private double g = 0;    // Kg/s

    // Properties
    public double MetabolicRate {get; private set; } //  Joule/m
    public double maxMetabolicRate { get; private set; }

    public double HeartRate{get; private set; }  //  bpm 
    public double maxHeartRate { get; private set; }
    public bool IsCalm { get; private set; }

    public float OxygenTime { get; private set; } // minutes to oxygen depletion
    public float maxOxygenTime { get; private set; }
    public bool timerIsRunning { get; private set; } //Timer control

    public double SuitPressure { get; private set; }
    public double maxSuitPressure { get; private set; }
    public double Temperature { get; private set; }
    public double maxTemperature { get; private set; }
    
    public double Humidity { get; private set; }

    public double maxHumidity;
    
    public double Radiation { get; private set; }

    public double maxRadiation;
    
    //Networking API reference
    [SerializeField] private NetworkAPIReceiver networkReceiver;

    // Update Counter
    [Tooltip("Interval between metric updates")]
    public float updateTime;

    private float counter;

    private void Awake()
    {
        //GridReference = GetComponent<Grid>;  
        //slope = 1; //degrees 
        timerIsRunning = true;
        IsCalm = true;
        maxHeartRate = 160;
        maxTemperature = 125;
        maxHumidity = 100;
        maxRadiation = 300;

    }

    void Start()
    {
        counter = 1f;
    }

    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= updateTime)
        {
            CalculateHeartRate();
            CalculateMetabolicRate();
            CalculateOxygen();
            CalculateSuitPress();
            CalculateVelocity();
            GetTemperature();
            GetHumidity();
            GetRadiation();
            
            counter = 0f;
        }
    }

    //Approximate astronaut velocity and metabolic rate relations (Santee et al.2001)

    private void CalculateVelocity()
    {
        if (slope < -20 || slope > 20)
            velocity = 0;
        else if (slope <= -10)
            velocity = 0.095 * slope + 1.95;
        else if (slope < 0)
            velocity = 0.05 * slope + 1.6;
        else if (slope < 6)
            velocity = -0.2 * slope + 1.6;
        else if (slope < 15)
            velocity = 0.039 * slope + 0.634;
        else
            velocity = 0.05;
    }

    private void CalculateMetabolicRate()
    {
        float slope_sin = Mathf.Sin(slope);
        float slope_cos = Mathf.Cos(slope);
        float slope_abs = Mathf.Abs(slope);

        double LevelEnergy = (3.28 * mass + 71.1) * (0.661 * velocity * slope_cos + 0.115);
        double SlopeEnergy;
        if (slope > 0)
            SlopeEnergy = 3.5 * mass * g * velocity * slope_sin;
        else if (slope < 0)
            SlopeEnergy = 2.4 * mass * g * velocity * slope_sin * Mathf.Pow(0.3f, (float)(slope_abs / 7.65));
        else
        {
            SlopeEnergy = 0;
        }


        MetabolicRate = LevelEnergy + SlopeEnergy;
    }

    private void CalculateOxygen()
    {
        if (timerIsRunning)
        {
            if (OxygenTime > 0)
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

    public void CalculateHeartRate()       //avg: 60-100 , range: 60-160
    {
        if (IsCalm)
        {
            //HeartRate = avgHeartRate + Mathf.Cos(Random.Range(-5,5));
            HeartRate = avgHeartRate + Random.Range(-5, 5);
        }

        //HeartRate = avgHeartRate + 50 + Mathf.Cos(Random.Range(-5,5));
        HeartRate = avgHeartRate + Random.Range(-5, 5);
    }

    private void CalculateSuitPress()
    {

    }

    private void GetTemperature()
    {
        Temperature = networkReceiver.tempValue;
    }
    
    private void GetHumidity()
    {
        Humidity = networkReceiver.humidityValue;
    }
    
    private void GetRadiation()
    {
        Radiation = networkReceiver.radiationValue;
    }
    
    

    // int GetManhattanDist(Node nodeA,Node nodeB){
    // int ix = Mathf.Abs(nodeA.iGridX - nodeB.iGridX);//x1-x2
    // int iy = Mathf.Abs(nodeA.iGridY - nodeB.iGridY);//y1-y2

    // return ix + iy;//Return the sum
    // }

}