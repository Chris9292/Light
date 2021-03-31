using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakingPhoto : MonoBehaviour
{
    TMP_Text photoText;
    public float dotInterval = 0.3f;
    public string phrase = "Taking Photo";

    void Awake()
    {
        photoText = gameObject.GetComponent<TMP_Text>();
        photoText.text = phrase;
    }

    float timer = 0f;
    int dotCounter = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dotInterval)
        {

            if (dotCounter > 3)
            {
                photoText.text = phrase + ".";
                dotCounter = 0;
            }
            else
                photoText.text += ".";
            
            dotCounter ++;
            timer = 0;
        }

    }
}
