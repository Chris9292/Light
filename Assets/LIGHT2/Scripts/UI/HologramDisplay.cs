using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramDisplay : MonoBehaviour
{
    public GameObject Display;
    public string[] PlaceableObjects;
    private GameObject[] gameObjects;
    //Transform DisplayTransform = Display.transform;
    public int CurrentHolo = 0;

    private void Awake()
    {
        gameObjects = new GameObject[PlaceableObjects.Length];
        for (int i = 0; i < PlaceableObjects.Length; i++)
        {
            gameObjects[i] = Resources.Load(PlaceableObjects[i]) as GameObject;
            GameObject.Instantiate(gameObjects[i], Display.transform, false);
        }
    }

//change to onEnable
    private void OnEnable()
    {
         //First Hologram in Hierarchy Enabled ; All the rest disabled
        for (int i=0; i<=Display.transform.childCount-1; i++){
        Display.transform.GetChild(i).gameObject.SetActive(false);
        }
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }

    

    public void DisplayNext(){
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo += 1;
        if (CurrentHolo >= Display.transform.childCount)
        {
        CurrentHolo = 0;
            }
         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    
    } 

    public void DisplayPrevious(){
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo -= 1;
        if (CurrentHolo < 0)
        {
        CurrentHolo = Display.transform.childCount - 1;
            }
         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }


    private void Update()
    {
        if (Display.transform.childCount < gameObjects.Length)
        {
            Debug.Log("c");
            InstantiatePlaceableObjects();
            gameObject.SetActive(false);
        }

    }

    private void InstantiatePlaceableObjects()
    {
        for (int i = 0; i < Display.transform.childCount; i++)
        {
            Debug.Log("a");
            Destroy(Display.transform.GetChild(i));
        }
        for (int i = 0; i < PlaceableObjects.Length; i++)
        {
            GameObject.Instantiate(gameObjects[i], Display.transform, false);
        }
    }
}
