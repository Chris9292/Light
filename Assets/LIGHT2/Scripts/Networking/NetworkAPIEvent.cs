using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkAPIEvent : MonoBehaviour
{
    public Button networkAPIButton;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("TriggerNetworkAPI", 2, 2);
    }

    private void TriggerNetworkAPI()
    {
        Debug.Log("Network API button clicked!");
        networkAPIButton.onClick.Invoke();
    }
}
