using Microsoft.MixedReality.Toolkit;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class DebugWindow : MonoBehaviour
{
    // Logs the gaze game object
    public bool GazeLog = false;
    // Time between each log
    public float TimeBetweenUpdates = 3f;
    private float Counter = 0f;

    TextMeshPro textMeshPro;

    // Use this for initialization
    void Awake()
    {
        textMeshPro = gameObject.GetComponent<TextMeshPro>();
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        if (textMeshPro.text.Length > 300)
        {
            textMeshPro.text = message + "\n";
        }
        else
        {
            textMeshPro.text += message + "\n";
        }
    }

    // Logs the gaze target if GazeLog is true
    void LogCurrentGazeTarget()
    {
        if (GazeLog && CoreServices.InputSystem.GazeProvider.GazeTarget)
        {
            Debug.Log("User gaze is currently over game object: "
                + CoreServices.InputSystem.GazeProvider.GazeTarget);
        }
    }

    private void Update()
    {
        if (Counter >= TimeBetweenUpdates)
        {
            LogCurrentGazeTarget();
            Counter -= TimeBetweenUpdates;
        }
        Counter += Time.deltaTime;
    }    
}