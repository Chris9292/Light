using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComTester : MonoBehaviour
{
    public TMP_Text logText;
    public void ChangeText(string host, string port, byte[] data)
    {
        logText.text = "GOT MESSAGE FROM: " + host + " on port " + port + " " + data.Length.ToString() + " bytes " + data.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
