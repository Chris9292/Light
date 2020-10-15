using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Display : MonoBehaviour
{
    public TMP_Text clock;
    public TMP_Text heartText;
    public TMP_Text positionText;
    public int heartRate;
    public GameObject icon;
    public int heartRateLow;
    public int heartRateHigh;
    public Transform position;
    public Transform previousPosition;
    private float distanceTraveled;
    private float delta;

    System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MyUpdate", 1.0f, 1.0f);
        heartRate = 90;
        heartRateHigh = 120;
        heartRateLow = 65;
        icon.SetActive(false);
        delta = 0.0f;
    }

    // Update is called once per frame
    void MyUpdate()
    {
        clock.text = DateTime.Now.ToString();
        

        positionText.text = position.position.ToString();

        distanceTraveled = Vector3.Distance(position.position, previousPosition.position);

        Debug.Log(distanceTraveled);

        heartRate += random.Next((int)(2*distanceTraveled>delta?0:(80 - heartRate))/5 , (int) distanceTraveled*10);
        heartText.text = heartRate.ToString();
        
        previousPosition.position = position.position;
        delta = distanceTraveled;

        if(heartRate>heartRateHigh || heartRate<heartRateLow){
            icon.SetActive(true);
        }else{
            icon.SetActive(false);
        }
    }
}
