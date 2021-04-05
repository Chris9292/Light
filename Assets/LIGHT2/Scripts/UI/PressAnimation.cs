using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedMove : MonoBehaviour
{
    [SerializeField] private Transform button;
    [SerializeField] private float MoveDuration;
    private float elapsed;
    private float startValue;
    private float endValue;
    float valueToLerp;
    


    private void Awake()
    {
        startValue = button.localScale.x;
        endValue = button.localScale.x - 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

     IEnumerator Move()
    {
        while(elapsed < MoveDuration)
        {
            valueToLerp = Mathf.Lerp(startValue,endValue,elapsed/MoveDuration);
                yield return null;
        }

        valueToLerp = endValue;
    }
}
