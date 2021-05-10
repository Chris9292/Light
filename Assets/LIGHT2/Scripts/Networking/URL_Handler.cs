using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class URL_Handler : MonoBehaviour
{
    public TMP_InputField urlInputField;
    public void ChangeURL()
    {
        // change static property
        WebRequest.url = urlInputField.text;
        GameObject.Find("ResponseArea").GetComponent<TextMeshProUGUI>().text = $"Sending Http GET Request to following URL: {WebRequest.url}\n\n";
    }
}
