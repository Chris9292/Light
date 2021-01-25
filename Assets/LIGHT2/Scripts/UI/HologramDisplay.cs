using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramDisplay : MonoBehaviour
{
    public GameObject Display;
    public string[] PlaceableObjects;
    private GameObject[] gameObjects;
    public int CurrentHolo = 0;

    private void Awake()
    {
        // Load the objects from resources defined in PlaceableObjects Array
        // Failing to find an objects throws an error
        gameObjects = new GameObject[PlaceableObjects.Length];
        for (int i = 0; i < PlaceableObjects.Length; i++)
        {
            gameObjects[i] = Resources.Load(PlaceableObjects[i]) as GameObject;

            if (gameObjects[i] == null)
                throw new UnityException("No prefab named '" + PlaceableObjects[i] + "' found in Resources");
        }
        // Instantiate the objects
        RefreshPlaceableObjects();
    }

    private void OnEnable()
    {
        //First Hologram in Hierarchy Enabled ; All the rest disabled
        for (int i=0; i<=Display.transform.childCount-1; i++)
        {
            Display.transform.GetChild(i).gameObject.SetActive(false);
        }
        
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }

    public void DisplayNext()
    {
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo ++ ;
        if (CurrentHolo >= Display.transform.childCount)
            CurrentHolo = 0;

         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    
    } 

    public void DisplayPrevious()
    {
        Display.transform.GetChild(CurrentHolo).gameObject.SetActive(false);

        CurrentHolo --;
        if (CurrentHolo < 0)
            CurrentHolo = Display.transform.childCount - 1;

         Display.transform.GetChild(CurrentHolo).gameObject.SetActive(true);
    }


/*    private void Update()
    {
        // If something has been placed close the menu and refresh the placeable objects
        if (Display.transform.childCount < gameObjects.Length)
        {
            gameObject.SetActive(false);
            RefreshPlaceableObjects();
        }

    }*/

    // Destroys every placeable object and reinstantiates them
    private void RefreshPlaceableObjects()
    {
        for (int i = 0; i < Display.transform.childCount; i++)
        {
            Destroy(Display.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < PlaceableObjects.Length; i++)
        {
            GameObject instantiatedObject = Instantiate(gameObjects[i], Display.transform, false);

           if (instantiatedObject.TryGetComponent(out Collider collider))
                collider.enabled = false;
        }
    }

    // TODO: Scale + Instantiate new Object -> Do not refresh
    public void PlaceHologram()
    {
        Transform hologramToPlace = Display.transform.GetChild(CurrentHolo);
        hologramToPlace.parent = null;

        if (!hologramToPlace.gameObject.TryGetComponent(out TapToPlace tapToPlace))
            throw new UnityException(hologramToPlace + " has no TapToPlace Component");
        else
        {

            gameObject.SetActive(false);
            tapToPlace.GetComponent<Collider>().enabled = true;
            hologramToPlace.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            tapToPlace.StartPlacement();
            RefreshPlaceableObjects();
        }
    }
}
