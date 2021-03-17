using UnityEngine;
using TMPro;

public class SpeechTest : MonoBehaviour
{
    public TMP_Text resultText;

    public void SpeechInputDetected()
    {
        resultText.text = "Speech Input Detected!";
    }

    public void ResetTest()
    {
        resultText.text = "Waiting for speech input...";
    }
}
