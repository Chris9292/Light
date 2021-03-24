using Microsoft.MixedReality.Toolkit;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    MainMenuManager menuManager;

    private void Awake()
    {
        menuManager = GameObject.FindGameObjectWithTag("MainMenuManager").GetComponent<MainMenuManager>();
    }

    // Distance from the user when the object is enabled
    public float SpawnDistanceFromUser = 2f;

    // Changes the transform to the direction of the gaze multiplied by a fixed distance
    public void ChangeSpawnLocation()
    {
        Vector3 gazeOrigin = CoreServices.InputSystem.GazeProvider.GazeOrigin;
        Vector3 gazeDirection = CoreServices.InputSystem.GazeProvider.GazeDirection;
        Vector3 spawnPosition =  gazeOrigin + (gazeDirection * SpawnDistanceFromUser);
        
        transform.position = spawnPosition;
    }
    private void OnEnable()
    {
        ChangeSpawnLocation();
        menuManager.ActiveMenu = true;
    }

    private void OnDisable()
    {
        menuManager.ActiveMenu = false;
    }
}