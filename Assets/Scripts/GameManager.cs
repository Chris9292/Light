using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject textObject;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(GameObject app)
    {
        Debug.Log(app.name);
        textObject.GetComponent<TextMeshProUGUI>().text = $"Your choice: {app.name}";
    }
}
