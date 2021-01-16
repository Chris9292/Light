using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class DebugWindow : MonoBehaviour
{
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
}